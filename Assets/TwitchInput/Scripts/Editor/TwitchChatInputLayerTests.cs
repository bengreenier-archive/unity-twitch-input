using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;
using System;
using System.Threading;
using TwitchInput.Irc;
using TwitchInput.Core;
using System.Collections.Generic;

public class TwitchInputLayerTests
{
    public class TCILMock : TwitchChatInputLayer
    {
        public Dictionary<string, bool> InputStates
        {
            get
            {
                return this.inputStates;
            }
        }

        public void InvokeUpdate()
        {
            Update();
        }
    }

    [Test]
	public void TwitchChatInputLayer_Success()
    {
        var instance = new GameObject().AddComponent<TCILMock>();

        Assert.AreEqual(0, instance.InputStates.Count);

        instance.HandleMessage("!input w");

        instance.InvokeUpdate();

        Assert.AreEqual(1, instance.InputStates.Count);
        Assert.IsTrue(instance.InputStates["W"]);

        instance.InvokeUpdate();

        Assert.AreEqual(1, instance.InputStates.Count);
        Assert.IsFalse(instance.InputStates["W"]);

        instance.InvokeUpdate();

        Assert.AreEqual(0, instance.InputStates.Count);
    }
}
