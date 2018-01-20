using UnityEngine;

namespace TwitchInput.Core
{
    public class TwInputConfigure : MonoBehaviour
    {
        public TwitchChatInputLayer InputLayer;

        private void Awake()
        {
            TwInput.Instance = this.InputLayer;
        }
    }
}
