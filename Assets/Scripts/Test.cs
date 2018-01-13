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
        //if (inputLayer.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log("walk forward, pressed");
        //}
        //else if (inputLayer.GetKeyUp(KeyCode.W))
        //{
        //    Debug.Log("walk forward, released");
        //}

        StartCoroutine(this.DoWork());
    }

    private int frame = 0;

    private IEnumerator DoWork()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("hi" + frame);

        frame++;

        yield return new WaitForEndOfFrame();
        Debug.Log("hi 2|" + frame);

        frame++;
    }
}