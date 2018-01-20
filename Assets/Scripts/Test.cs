using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TwitchInput.Core;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool andPatch = false;

    private void Start()
    {
        // makes testing easier as i focus/unfocus the editor
        Application.runInBackground = true;

        if (andPatch)
        {
            TwInput.Patch();
        }
    }

    private void Update()
    {
        if (TwInput.GetKeyDown(KeyCode.W))
        {
            Debug.Log("tw: walk forward, pressed");
        }
        else if (TwInput.GetKeyUp(KeyCode.W))
        {
            Debug.Log("tw: walk forward, released");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("in: walk forward, pressed");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("in: walk forward, released");
        }
    }
}