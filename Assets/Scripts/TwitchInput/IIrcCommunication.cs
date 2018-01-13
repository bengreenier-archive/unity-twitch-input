
using System;
using System.IO;

public interface IIrcCommunication
{
    bool Connected { get; }

    void Connect(string hostname, int port);
    void Close();
    Stream GetStream();
}
