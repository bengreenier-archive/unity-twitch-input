using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityTwitchClient : MonoBehaviour
{
    [Serializable]
    public class UserMessageEvent : UnityEvent<string>
    {
    }

    public TwitchAuthentication AuthenticationToken;
    public string Username;
    public string Channel;
    public UserMessageEvent OnUserMessage;
    
    private TwitchIrc irc;
    private Queue<string> userMessageQueue;

    private void Start()
    {
        this.irc = new TwitchIrc(new TcpIrcCommunication(),
            new Uri("irc://irc.twitch.tv:6667"),
            this.AuthenticationToken.Token,
            this.Username);

        this.userMessageQueue = new Queue<string>();

        this.irc.Connected += () =>
        {
            this.irc.JoinChannel(this.Channel);
        };

        this.irc.MessageReceived += (string msg) =>
        {
            Debug.Log("msg: " + msg);
        };

        this.irc.UserMessage += (string userMessage) =>
        {
            lock (this.userMessageQueue)
            {
                this.userMessageQueue.Enqueue(userMessage);
            }
        };

        this.irc.Connect();
    }

    private void Update()
    {
        lock (this.userMessageQueue)
        {
            while (this.userMessageQueue.Count > 0)
            {
                this.OnUserMessage.Invoke(this.userMessageQueue.Dequeue());
            }
        }
    }

    private void OnDestroy()
    {
        // clean this up
        this.irc.Dispose();
    }
}
