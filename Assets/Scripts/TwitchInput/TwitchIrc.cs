using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

/// <summary>
/// Specialized Twitch IRC Client
/// </summary>
/// <remarks>
/// See <c>https://dev.twitch.tv/docs/irc</c> for details, but basically this adds the necessary login
/// steps, ping ponging, and rate limiting to communicate with twitch irc for extended periods of time
/// </remarks>
public class TwitchIrc : IrcClient
{
    /// <summary>
    /// Global rate limiting behaviors
    /// </summary>
    public enum GlobalRateLimit
    {
        /// <summary>
        /// No rate limiting
        /// </summary>
        None,

        /// <summary>
        /// User rate limiting
        /// </summary>
        User,

        /// <summary>
        /// Mod rate limiting
        /// </summary>
        Mod,

        /// <summary>
        /// Known bot rate limiting
        /// </summary>
        KnownBot,

        /// <summary>
        /// Verified bot rate limiting
        /// </summary>
        /// <remarks>
        /// This will consume quite a bit of memory, as it keeps a buffer of 7500 messages
        /// </remarks>
        VerifiedBot
    }
    
    /// <summary>
    /// Represents a message that has been queued for rate limiting reasons
    /// </summary>
    private struct MessageQueueEntry
    {
        /// <summary>
        /// The requested send time
        /// </summary>
        public DateTime RequestedSendTime;

        /// <summary>
        /// Was the message sent already
        /// </summary>
        public bool WasSent;

        /// <summary>
        /// Message contents
        /// </summary>
        public string Message;
    }

    /// <summary>
    /// This event is fired with the contents of a user message
    /// whenever a user sends something in chat
    /// </summary>
    public event Action<string> UserMessage;

    /// <summary>
    /// Backing field for <see cref="GlobalRateLimiting"/>
    /// </summary>
    private GlobalRateLimit globalRateLimit;

    /// <summary>
    /// Indicates the current rate limiting in effect
    /// </summary>
    public GlobalRateLimit GlobalRateLimiting
    {
        get
        {
            return this.globalRateLimit;
        }

        set
        {
            this.globalRateLimit = value;

            // toggle on/off the message queue checker thread (for buffering rate limited messages)
            if (value == GlobalRateLimit.None && this.messageQueueChecker != null)
            {
                this.messageQueueChecker.Dispose();
            }
            else if (value != GlobalRateLimit.None && this.messageQueueChecker == null)
            {
                this.messageQueueChecker = new Timer(this.CheckMessageQueue, null, 0, 30);
            }
        }
    }

    /// <summary>
    /// Internal message queue used for rate limit buffering
    /// </summary>
    private Queue<MessageQueueEntry> messageQueue;

    /// <summary>
    /// Internal timer used for checking and resending buffered messages for rate limiting
    /// </summary>
    private Timer messageQueueChecker;

    /// <summary>
    /// Default ctor
    /// </summary>
    /// <param name="uri">uri to connect to</param>
    /// <param name="oauthToken">oauth token to use</param>
    /// <param name="userName">username to use</param>
    /// <remarks>
    /// see <c>https://dev.twitch.tv/docs/irc</c> for how to get a token
    /// </remarks>
    public TwitchIrc(Uri uri, string oauthToken, string userName) : base(uri)
    {
        this.globalRateLimit = GlobalRateLimit.None;
        this.messageQueue = new Queue<MessageQueueEntry>();

        this.Connected += () =>
        {
            if (!string.IsNullOrEmpty(oauthToken))
            {
                this.Send("PASS oauth:" + oauthToken);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                this.Send("NICK " + userName);
            }
        };

        Regex messageExtractor = new Regex("PRIVMSG\\s#.+\\s:(.+)");

        this.MessageReceived += (string msg) =>
        {
            if (msg.StartsWith("PING "))
            {
                // we bypass this.send, because we don't want ping pong to be subject to rate limiting
                base.Send(msg.Replace("PING ", "PONG "));
            }

            var messageMatches = messageExtractor.Match(msg);

            if (messageMatches.Success)
            {
                var captured = messageExtractor.Match(msg).Groups[1];

                // emit the user message event
                SafeFireEvent<string>(this.UserMessage, captured.Value);
            }
        };
    }
    
    /// <summary>
    /// Joins a channel on twitch
    /// </summary>
    /// <param name="channelName">the channel name</param>
    public void JoinChannel(string channelName)
    {
        this.Send("CAP REQ :twitch.tv/membership");
        this.Send("JOIN #" + channelName);
        this.Send("CAP REQ :twitch.tv/tags");
        this.Send("CAP REQ :twitch.tv/commands");
    }

    /// <inheritdocs/>
    public override void Send(string msg)
    {
        if (this.GlobalRateLimiting != GlobalRateLimit.None)
        {
            var queueEntry = new MessageQueueEntry()
            {
                Message = msg,
                RequestedSendTime = DateTime.UtcNow,
                WasSent = true
            };

            if (this.RateLimitExceeded())
            {
                // we're going to buffer it and send when we can
                queueEntry.WasSent = false;

                this.messageQueue.Enqueue(queueEntry);
                return;
            }

        }

        base.Send(msg);
    }

    /// <summary>
    /// Internal checker for rate limiting rules
    /// </summary>
    /// <returns>has the rate limit been exceeded</returns>
    private bool RateLimitExceeded()
    {
        // the rate limiting rules per https://dev.twitch.tv/docs/irc
        //
        // TODO(bengreenier): the values encapsulated in the rules here are duplicated below. they should not be
        return
            (this.GlobalRateLimiting == GlobalRateLimit.User && this.messageQueue.Count(e => e.RequestedSendTime >= DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(30))) > 20) ||
            (this.GlobalRateLimiting == GlobalRateLimit.Mod && this.messageQueue.Count(e => e.RequestedSendTime >= DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(30))) > 100) ||
            (this.GlobalRateLimiting == GlobalRateLimit.KnownBot && this.messageQueue.Count(e => e.RequestedSendTime >= DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(30))) > 50) ||
            (this.GlobalRateLimiting == GlobalRateLimit.VerifiedBot && this.messageQueue.Count(e => e.RequestedSendTime >= DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(30))) > 7500);
    }

    /// <summary>
    /// Internal procedure for <see cref="messageQueueChecker"/>
    /// </summary>
    /// <param name="state"><c>null</c></param>
    private void CheckMessageQueue(object state)
    {
        // remove uneeded sent messages (timing data is no longer relevant)
        int clearCount = 0;

        if (this.GlobalRateLimiting == GlobalRateLimit.User && this.messageQueue.Count(e => e.WasSent) > 20)
        {
            clearCount = 20;
        }
        else if (this.GlobalRateLimiting == GlobalRateLimit.Mod && this.messageQueue.Count(e => e.WasSent) > 100)
        {
            clearCount = 100;
        }
        else if (this.GlobalRateLimiting == GlobalRateLimit.KnownBot && this.messageQueue.Count(e => e.WasSent) > 50)
        {
            clearCount = 50;
        }
        else if (this.GlobalRateLimiting == GlobalRateLimit.VerifiedBot && this.messageQueue.Count(e => e.WasSent) > 7500)
        {
            clearCount = 7500;
        }

        // actual removal
        for (var i = 0; i < clearCount; i++)
        {
            this.messageQueue.Dequeue();
        }

        // try to send unsent messages
        //
        // TODO(bengreenier): this could be more performant if we were aware of how many we could send instead of potential requeuing
        var immutableSendArr = this.messageQueue.Where(m => !m.WasSent).ToArray();

        foreach (var entry in immutableSendArr)
        {
            this.Send(entry.Message);
        }
    }

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (this.messageQueueChecker != null)
                {
                    this.messageQueueChecker.Dispose();
                }
            }

            disposedValue = true;
        }

        base.Dispose(disposing);
    }

    #endregion
}