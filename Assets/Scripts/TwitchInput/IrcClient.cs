using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

/// <summary>
/// TCP IRC Client
/// </summary>
public class IrcClient : IDisposable
{
    /// <summary>
    /// Indicates client is connected
    /// </summary>
    public event Action Connected;

    /// <summary>
    /// Indicates client is disconnected
    /// </summary>
    public event Action Disconnected;

    /// <summary>
    /// Indicates client received a message
    /// </summary>
    public event Action<string> MessageReceived;

    /// <summary>
    /// The Uri the client is configured to connect to 
    /// </summary>
    public Uri Uri
    {
        get;
        private set;
    }

    /// <summary>
    /// Internal tcp client used for connections
    /// </summary>
    private IIrcCommunication client;

    /// <summary>
    /// Internal streamreader for the tcp stream
    /// </summary>
    /// <remarks>
    /// This is a member variable since closing streams closes the connection
    /// so we instead Flush, not Close
    /// </remarks>
    private StreamReader clientReader;

    /// <summary>
    /// Internal streamwriter for the tcp stream
    /// </summary>
    /// <remarks>
    /// This is a member variable since closing streams closes the connection
    /// so we instead Flush, not Close
    /// </remarks>
    private StreamWriter clientWriter;

    /// <summary>
    /// Internal thread used for handling messages
    /// </summary>
    private Thread messageHandler;

    /// <summary>
    /// Default ctor
    /// </summary>
    /// <param name="commClient">the underlying communications client</param>
    /// <param name="uri">Uri to connect to</param>
    /// <remarks>
    /// Note, you must call <see cref="Connect"/> to connect
    /// </remarks>
    public IrcClient(IIrcCommunication commClient, Uri uri)
    {
        this.Uri = uri;
        this.client = commClient;
    }

    /// <summary>
    /// Connects the client to <see cref="Uri"/>
    /// </summary>
    /// <remarks>
    /// Triggers <see cref="Connected"/>
    /// </remarks>
    public void Connect()
    {
        if (this.client.Connected)
        {
            throw new InvalidOperationException("Already connected");
        }
        
        this.client.Connect(this.Uri.DnsSafeHost, this.Uri.Port);

        var stream = this.client.GetStream();

        this.clientReader = new StreamReader(stream);
        this.clientWriter = new StreamWriter(stream);

        this.messageHandler = new Thread(HandleMessage);
        this.messageHandler.Start();
        
        SafeFireEvent(Connected);
    }

    /// <summary>
    /// Disconnects the client from <see cref="Uri"/>
    /// </summary>
    /// <remarks>
    /// Triggers <see cref="Disconnected"/>
    /// </remarks>
    public void Disconnect()
    {
        if (!this.client.Connected)
        {
            throw new InvalidOperationException("Not connected");
        }

        this.client.Close();
        this.messageHandler.Join();
        this.clientReader.Dispose();
        this.clientWriter.Dispose();

        SafeFireEvent(Disconnected);
    }

    /// <summary>
    /// Send a message
    /// </summary>
    /// <param name="msg">message to send</param>
    public virtual void Send(string msg)
    {
        if (!this.client.Connected)
        {
            throw new InvalidOperationException("Not connected");
        }

        this.clientWriter.WriteLine(msg);
        this.clientWriter.Flush();
    }

    /// <summary>
    /// Thread procedure for handling messages
    /// </summary>
    protected void HandleMessage()
    {
        while (this.client.Connected)
        {
            var msg = this.clientReader.ReadLine();
            SafeFireEvent(MessageReceived, msg);
        }
    }

    /// <summary>
    /// Helpers to safely fire an event (if it exists)
    /// </summary>
    /// <param name="evt">event to fire</param>
    protected void SafeFireEvent(Action evt)
    {
        if (evt != null)
        {
            evt();
        }
    }

    /// <summary>
    /// Helpers to safely fire an event (if it exists)
    /// </summary>
    /// <typeparam name="TArg">argument type</typeparam>
    /// <param name="evt">event to fire</param>
    /// <param name="arg">event argument</param>
    protected void SafeFireEvent<TArg>(Action<TArg> evt, TArg arg)
    {
        evt(arg);
    }

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.Disconnect();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}