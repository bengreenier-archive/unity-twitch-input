using System.Collections;
using System.Collections.Generic;
using TwitchInput.Core;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor extension for UnityTwitchClient
/// </summary>
/// <remarks>
/// This enables us to raise message events
/// </remarks>
[CustomEditor(typeof(UnityTwitchChatClient))]
public class UnityTwitchClientEditor : Editor
{
    private string triggerText = "";
    private int triggerAmount = 1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var client = target as UnityTwitchChatClient;
        var evt = client.OnUserMessage;

        GUI.enabled = Application.isPlaying && evt.GetPersistentEventCount() > 0;

        triggerText = GUILayout.TextField(triggerText);

        GUILayout.BeginHorizontal();

        GUILayout.Label(triggerAmount.ToString(), new GUILayoutOption[]{
            GUILayout.Width(30)
        });

        triggerAmount = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)triggerAmount, 0, 100));

        if (GUILayout.Button("Raise OnUserMessage"))
        {
            for (var i = 0; i < triggerAmount; i++)
            {
                evt.Invoke(triggerText);
            }
        }

        GUILayout.EndHorizontal();
    }
}