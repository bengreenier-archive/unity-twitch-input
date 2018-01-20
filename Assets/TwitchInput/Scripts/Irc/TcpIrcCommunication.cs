
using System.IO;
using System.Net.Sockets;

namespace TwitchInput.Irc
{
    class TcpIrcCommunication : IIrcCommunication
    {
        private TcpClient client = new TcpClient();

        public bool Connected
        {
            get
            {
                return client.Connected;
            }
        }

        public void Close()
        {
            client.Close();
        }

        public void Connect(string hostname, int port)
        {
            client.Connect(hostname, port);
        }

        public Stream GetStream()
        {
            return client.GetStream();
        }
    }
}
