using System;
using TwitchInput.Internal;
using UnityEngine;

namespace TwitchInput.Core
{
    /// <summary>
    /// TwitchInput singleton
    /// </summary>
    /// <remarks>
    /// This shims input to a TwitchChatInputLayer, if you do not provide
    /// an instance to <see cref="Instance"/> then one will be generated for you
    /// </remarks>
    class TwInput : InputShim<TwInput>
    {
        private bool wasChatLayerGenerated = false;
        private IInput inputProvider;

        protected override IInput InstanceGet()
        {
            if (inputProvider == null)
            {
                inputProvider = new GameObject("TwitchInput.ChatLayer" + Guid.NewGuid())
                    .AddComponent<TwitchChatInputLayer>();
                wasChatLayerGenerated = true;
            }

            return inputProvider;
        }

        protected override void InstanceSet(IInput instance)
        {
            if (wasChatLayerGenerated)
            {
                GameObject.Destroy((inputProvider as TwitchChatInputLayer).gameObject);
                wasChatLayerGenerated = false;
            }

            inputProvider = instance;
        }

        private static bool isPatched = false;

        /// <summary>
        /// Determines if <see cref="Input"/> is patched with <see cref="TwInput"/>
        /// </summary>
        public static bool IsPatched
        {
            get
            {
                return isPatched;
            }
        }

        /// <summary>
        /// BETA FEATURE: Patches <see cref="Input"/> with <see cref="TwInput"/>
        /// </summary>
        /// <remarks>
        /// This will redirect all <see cref="Input"/> calls to first
        /// try a <see cref="TwInput"/> call - if <see cref="TwInput"/>
        /// throws a <see cref="NotImplementedException"/> we fallback to <see cref="Input"/>
        /// </remarks>
        public static void Patch()
        {
            // we can only patch once
            if (IsPatched)
            {
                return;
            }

            InputPatcher.Patch();
            isPatched = true;
        }
    }
}
