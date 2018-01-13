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

        this.commands.Enqueue(userMessage);

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
        if (this.commands.Count == 0)
        {
            return;
        }

        var command = this.commands.Dequeue();
        
        StartCoroutine(this.ResetKey(command));
    }

    private IEnumerator ResetKey(string command)
    {
        Debug.Log("end of frame");
        yield return new WaitForEndOfFrame();
        Debug.Log("top of frame");

        //this.keyStates[command] = true;

        Debug.Log("end of frame");
        yield return new WaitForEndOfFrame();
        Debug.Log("top of frame");

        //this.keyStates[command] = false;
    }
}