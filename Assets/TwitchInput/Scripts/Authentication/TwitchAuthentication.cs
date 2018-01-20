using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace TwitchInput.Authentication
{
    /// <summary>
    /// Basic Unity client_credential token provider for twitch
    /// </summary>
    /// <remarks>
    /// See <c>https://dev.twitch.tv/docs/authentication</c> for more info
    ///
    /// Relies on <see cref="JsonUtility" /> for json parsing
    /// </remarks>
    [CreateAssetMenu]
    public class TwitchAuthentication : ScriptableObject
    {
        public string Token;
    }
}
