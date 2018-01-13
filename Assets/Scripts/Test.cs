using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TwitchInputLayer inputLayer;

    private void Start()
    {
        // makes testing easier as i focus/unfocus the editor
        Application.runInBackground = true;
    }

    private void Update()
    {
        if (inputLayer.GetKeyDown(KeyCode.W))
        {
            Debug.Log("walk forward, pressed");
        }
        else if (inputLayer.GetKeyUp(KeyCode.W))
        {
            Debug.Log("walk forward, released");
        }
    }
}