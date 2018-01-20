using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitchInput.Internal;
using UnityEngine;

namespace TwitchInput.Core
{
    /// <summary>
    /// Converts twitch chat messages to input.
    /// </summary>
    /// <remarks>
    /// You must call <see cref="HandleMessage(string)"/> with chat messages
    /// To support additional input features, subclass and override an <see cref="IInput"/> member
    /// </remarks>
    /// <example></example>
    /// Supports chat messages of the following example format:
    /// 
    /// !input <keyName>||<keyLiteral>||M<mouseButtonIndex>||<ButtonName>
    /// 
    /// !input "w"
    /// !input W
    /// !input M1
    /// !input Forward
    /// 
    /// You can then query for these using the following methods:
    /// GetKeyDown
    /// GetKeyUp
    /// GetMouseButtonDown
    /// GetMouseButtonUp
    /// GetButtonDown
    /// GetButtonUp
    /// </example>
    public class TwitchChatInputLayer : MonoBehaviour, IInput
    {
        /// <summary>
        /// Prefix that messages should contain in order to be considered input
        /// </summary>
        public string CommandPrefix = "!input";

        /// <summary>
        /// Number of frames for which input is considered to be occuring
        /// </summary>
        public int CommandDuration = 1;

        /// <summary>
        /// Prefix for input commands representing mouse buttons
        /// </summary>
        public string MousePrefix = "M";

        private enum KeyDirection
        {
            None = 0,
            Up,
            Down
        }

        private Dictionary<string, Queue<KeyDirection>> keyCommands = new Dictionary<string, Queue<KeyDirection>>();
        protected Dictionary<string, bool> inputStates = new Dictionary<string, bool>();

        /// <summary>
        /// Handler for twitch chat events. should be wired to something like <see cref="UnityTwitchChatClient.OnUserMessage"/>
        /// via the editor
        /// </summary>
        /// <remarks>
        /// This abstraction is designed to improve testability - we can trigger the chat layer via this handler
        /// at any point, as opposed to being coupled directly to the <see cref="UnityTwitchChatClient"/>
        /// </remarks>
        /// <param name="userMessage">twitch chat message</param>
        public virtual void HandleMessage(string userMessage)
        {
            // no need to process messages that aren't input commands
            if (!userMessage.StartsWith(this.CommandPrefix))
            {
                return;
            }

            // strip command prefix and whitespace
            userMessage = userMessage.Substring(this.CommandPrefix.Length).Trim();
            userMessage = userMessage.ToUpper();

            if (!keyCommands.ContainsKey(userMessage))
            {
                keyCommands[userMessage] = new Queue<KeyDirection>();
            }

            // stretch commands for command duration
            for (var i = 0; i < this.CommandDuration; i++)
            {
                // queue a keypress for command duration
                keyCommands[userMessage].Enqueue(KeyDirection.Down);
            }

            // queue a keyrelease, key clear
            //
            // this is intentionally verbose as it gives us the freedom
            // to interpret commands as key-state holds (not always press, clear)
            keyCommands[userMessage].Enqueue(KeyDirection.Up);
            keyCommands[userMessage].Enqueue(KeyDirection.None);
        }

        protected void Update()
        {
            foreach (var key in keyCommands.Keys)
            {
                if (keyCommands[key].Count > 0)
                {
                    var command = keyCommands[key].Dequeue();
                    if (command == KeyDirection.Down)
                    {
                        inputStates[key] = true;
                    }
                    else if (command == KeyDirection.Up)
                    {
                        inputStates[key] = false;
                    }
                    else
                    {
                        inputStates.Remove(key);
                    }
                }
            }
        }

        #region input

        public virtual Vector3 acceleration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int accelerationEventCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual AccelerationEvent[] accelerationEvents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool anyKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool anyKeyDown
        {
            get
            {
                return this.inputStates.Count(s => s.Value) > 0;
            }
        }

        public virtual bool backButtonLeavesApp
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Compass compass
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool compensateSensors
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Vector2 compositionCursorPos
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string compositionString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual DeviceOrientation deviceOrientation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool eatKeyPressOnTextFieldFocus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Gyroscope gyro
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IMECompositionMode imeCompositionMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool imeIsSelected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string inputString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool isGyroAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual LocationService location
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Vector3 mousePosition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool mousePresent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Vector2 mouseScrollDelta
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool multiTouchEnabled
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool simulateMouseWithTouches
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool stylusTouchSupported
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int touchCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Touch[] touches
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool touchPressureSupported
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool touchSupported
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual AccelerationEvent GetAccelerationEvent(int index)
        {
            throw new NotImplementedException();
        }

        public virtual float GetAxis(string axisName)
        {
            throw new NotImplementedException();
        }

        public virtual float GetAxisRaw(string axisName)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetButton(string buttonName)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetButtonDown(string buttonName)
        {
            return this.inputStates.ContainsKey(buttonName) && this.inputStates[buttonName];
        }

        public virtual bool GetButtonUp(string buttonName)
        {
            return this.inputStates.ContainsKey(buttonName) && !this.inputStates[buttonName];
        }

        public virtual string[] GetJoystickNames()
        {
            throw new NotImplementedException();
        }

        public virtual bool GetKeyDown(KeyCode key)
        {
            return this.inputStates.ContainsKey(key.ToString()) && this.inputStates[key.ToString()];
        }

        public virtual bool GetKeyUp(KeyCode key)
        {
            return this.inputStates.ContainsKey(key.ToString()) && !this.inputStates[key.ToString()];
        }

        public bool GetKey(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetKeyDown(string name)
        {
            return this.inputStates.ContainsKey(name) && this.inputStates[name];
        }

        public virtual bool GetKeyUp(string name)
        {
            return this.inputStates.ContainsKey(name) && !this.inputStates[name];
        }

        public virtual bool GetMouseButton(int button)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetMouseButtonDown(int button)
        {
            return this.inputStates.ContainsKey(MousePrefix + button) && this.inputStates[MousePrefix + button];
        }

        public virtual bool GetMouseButtonUp(int button)
        {
            return this.inputStates.ContainsKey(MousePrefix + button) && !this.inputStates[MousePrefix + button];
        }

        public virtual Touch GetTouch(int index)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsJoystickPreconfigured(string joystickName)
        {
            throw new NotImplementedException();
        }

        public virtual void ResetInputAxes()
        {
            this.inputStates.Clear();
        }

        #endregion
    }
}
