using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;
using System;
using System.Threading;

public class TwitchIrcClientTests
{
    public class TestIrcCommunication : IIrcCommunication
    {
        public bool Connected
        {
            get;
            set;
        }

        public void Close()
        {
        }

        public void Connect(string hostname, int port)
        {
            this.Connected = true;
        }

        public Stream Stream;

        public Stream GetStream()
        {
            return Stream;
        }
    }


    [Test]
	public void TwitchIrcClient_Connected_Success()
    {
        var expectedAuth = "testauth";
        var expectedUser = "testuser";
        var expectedData = string.Format("PASS oauth:{1}{0}NICK {2}{0}", Environment.NewLine, expectedAuth, expectedUser);

        var testStream = new MemoryStream();
        var testCommClient = new TestIrcCommunication();
        
        testCommClient.Stream = testStream;

        TwitchIrc instance = new TwitchIrc(testCommClient, new Uri("test://unit.test"), expectedAuth, expectedUser);

        instance.Connect();

        testStream.Seek(0, SeekOrigin.Begin);

        using (var sr = new StreamReader(testStream))
        {
            var testData = sr.ReadToEnd();

            Assert.AreEqual(expectedData, testData);
        }
	}

    /// <summary>
    /// something is going on with threading, seems like things work with the debugger attached only
    /// </summary>
    [Test]
    public void TwitchIrcClient_PingPong_Success()
    {
        //var expectedData = string.Format("PONG :tmi.twitch.tv{0}", Environment.NewLine);

        //var testStream = new MemoryStream();
        //var testCommClient = new TestIrcCommunication();

        //testCommClient.Stream = testStream;
        
        //TwitchIrc instance = new TwitchIrc(testCommClient, new Uri("test://unit.test"), "anyAuth", "anyUser");

        //instance.Connect();

        //var prePingPosition = testStream.Position;
        //var pingMessage = System.Text.Encoding.ASCII.GetBytes("PING :tmi.twitch.tv" + Environment.NewLine);
        //testStream.Write(pingMessage, 0, pingMessage.Length);
        //testStream.Seek(prePingPosition, SeekOrigin.Begin);

        //bool messageReceived = false;
        //string messageContents = null;
        
        //instance.MessageReceived += (string msg) =>
        //{
        //    messageReceived = true;
        //    messageContents = msg;
        //};

        //Thread.Sleep(5000);

        //Assert.IsTrue(messageReceived);
        //Assert.AreEqual(expectedData, messageContents);

        //testStream.Seek(endOfPing, SeekOrigin.Begin);

        //using (var sr = new StreamReader(testStream))
        //{
        //    var testData = sr.ReadToEnd();

        //    Assert.AreEqual(expectedData, testData);
        //}
    }
}
