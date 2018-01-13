using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TwitchInputLayer : MonoBehaviour
{
    /// <summary>
    /// Prefix that messages should contain in order to be considered input
    /// </summary>
    public string CommandPrefix = "!input";

    /// <summary>
    /// Number of frames for which input is considered to be occuring
    /// </summary>
    public int CommandDuration = 1;

    private Queue<string> commands = new Queue<string>();
    private Dictionary<string, bool> keyStates = new Dictionary<string, bool>();
    private List<string> updatingKeys = new List<string>();

    public void HandleMessage(string userMessage)
    {
        // no need to process messages that aren't input commands
        if (!userMessage.StartsWith(this.CommandPrefix))
        {
            return;
        }

        // strip command prefix and whitespace
        userMessage = userMessage.Substring(this.CommandPrefix.Length).Trim();

        // TODO(bengreenier): make this configurable
        userMessage = userMessage.ToUpper();

        lock (this.commands)
        {
            this.commands.Enqueue(userMessage);
        }

        Debug.Log("TwitchInputLayer: " + userMessage);
    }
    
    public bool GetKeyDown(KeyCode key)
    {
        return this.keyStates.ContainsKey(key.ToString()) && this.keyStates[key.ToString()];
    }

    public bool GetKeyUp(KeyCode key)
    {
        return this.keyStates.ContainsKey(key.ToString()) && !this.keyStates[key.ToString()];
    }
    
    private void Update()
    {
        lock (this.commands)
        {
            if (this.commands.Count == 0)
            {
                return;
            }

            var command = this.commands.Dequeue();

            StartCoroutine(this.UpdateKey(command));
        }
    }

    private IEnumerator UpdateKey(string key)
    {
        // block if a key update is already in progress for this key
        // until it is complete
        //while (this.updatingKeys.Contains(key))
        //{
        //    yield return null;
        //}

        //// make the key as in update to block others
        //lock (this.updatingKeys)
        //{
        //    this.updatingKeys.Add(key);
        //}

        // wait one frame
        yield return new WaitForEndOfFrame();

        // activate key
        this.keyStates[key] = true;

        // wait command duration amount of frames
        for (var i = 0; i < this.CommandDuration; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        // deactivate key
        this.keyStates[key] = false;

        // wait one frame
        yield return new WaitForEndOfFrame();

        // remove key state
        this.keyStates.Remove(key);

        // unblock the key, we're complete
        //lock (this.updatingKeys)
        //{
        //    this.updatingKeys.Remove(key);
        //}
    }
}