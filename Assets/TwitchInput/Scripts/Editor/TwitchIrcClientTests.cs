using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;
using System;
using System.Threading;
using TwitchInput.Irc;

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
    
    private TwitchIrc instance;
    
    [PostTest]
    public void Cleanup()
    {
        if (instance != null)
        {
            instance.Dispose();
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

        instance = new TwitchIrc(testCommClient, new Uri("test://unit.test"), expectedAuth, expectedUser);

        instance.Connect();

        testStream.Seek(0, SeekOrigin.Begin);

        using (var sr = new StreamReader(testStream))
        {
            var testData = sr.ReadToEnd();

            Assert.AreEqual(expectedData, testData);
        }
	}
}
