using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TwitchInput.Internal
{
    /// <summary>
    /// A static input shim for instanced <see cref="IInput"/> providers
    /// </summary>
    /// <remarks>
    /// You must set <see cref="Instance"/> for this to work!
    /// </remarks>
    public class InputShim<TShim> where TShim : InputShim<TShim>, new()
    {
        protected static InputShim<TShim> objectInstance;

        /// <summary>
        /// Must be set in order for the shim to work
        /// </summary>
        public static IInput Instance
        {
            get
            {
                if (objectInstance == null)
                {
                    objectInstance = new TShim();
                }

                return objectInstance.InstanceGet();
            }

            set
            {
                if (objectInstance == null)
                {
                    objectInstance = new TShim();
                }

                objectInstance.InstanceSet(value);
            }
        }

        private IInput inputInstance;

        protected virtual IInput InstanceGet() { return inputInstance; }
        protected virtual void InstanceSet(IInput instance) { inputInstance = instance; }

        public static Vector3 acceleration { get { return Instance.acceleration; } }
        public static int accelerationEventCount { get { return Instance.accelerationEventCount; } }
        public static AccelerationEvent[] accelerationEvents { get { return Instance.accelerationEvents; } }
        public static bool anyKey { get { return Instance.anyKey; } }
        public static bool anyKeyDown { get { return Instance.anyKeyDown; } }
        public static bool backButtonLeavesApp { get { return Instance.backButtonLeavesApp; } set { Instance.backButtonLeavesApp = value; } }
        public static Compass compass { get { return Instance.compass; } }
        public static bool compensateSensors { get { return Instance.compensateSensors; } set { Instance.compensateSensors = value; } }
        public static Vector2 compositionCursorPos { get { return Instance.compositionCursorPos; } set { Instance.compositionCursorPos = value; } }
        public static string compositionString { get { return Instance.compositionString; } }
        public static DeviceOrientation deviceOrientation { get { return Instance.deviceOrientation; } }
        public static bool eatKeyPressOnTextFieldFocus { get { return Instance.eatKeyPressOnTextFieldFocus; } set { Instance.eatKeyPressOnTextFieldFocus = value; } }
        public static Gyroscope gyro { get { return Instance.gyro; } }
        public static IMECompositionMode imeCompositionMode { get { return Instance.imeCompositionMode; } set { Instance.imeCompositionMode = value; } }
        public static bool imeIsSelected { get { return Instance.imeIsSelected; } }
        public static string inputString { get { return Instance.inputString; } }
        public static bool isGyroAvailable { get { return Instance.isGyroAvailable; } }
        public static LocationService location { get { return Instance.location; } }
        public static Vector3 mousePosition { get { return Instance.mousePosition; } }
        public static bool mousePresent { get { return Instance.mousePresent; } }
        public static Vector2 mouseScrollDelta { get { return Instance.mouseScrollDelta; } }
        public static bool multiTouchEnabled { get { return Instance.multiTouchEnabled; } set { Instance.multiTouchEnabled = value; } }
        public static bool simulateMouseWithTouches { get { return Instance.simulateMouseWithTouches; } set { Instance.simulateMouseWithTouches = value; } }
        public static bool stylusTouchSupported { get { return Instance.stylusTouchSupported; } }
        public static int touchCount { get { return Instance.touchCount; } }
        public static Touch[] touches { get { return Instance.touches; } }
        public static bool touchPressureSupported { get { return Instance.touchPressureSupported; } }
        public static bool touchSupported { get { return Instance.touchSupported; } }
        public static AccelerationEvent GetAccelerationEvent(int index) { return Instance.GetAccelerationEvent(index); }
        public static float GetAxis(string axisName) { return Instance.GetAxis(axisName); }
        public static float GetAxisRaw(string axisName) { return Instance.GetAxisRaw(axisName); }
        public static bool GetButton(string buttonName) { return Instance.GetButton(buttonName); }
        public static bool GetButtonDown(string buttonName) { return Instance.GetButtonDown(buttonName); }
        public static bool GetButtonUp(string buttonName) { return Instance.GetButtonUp(buttonName); }
        public static string[] GetJoystickNames() { return Instance.GetJoystickNames(); }
        public static bool GetKey(string name) { return Instance.GetKey(name); }
        public static bool GetKeyDown(KeyCode key) { return Instance.GetKeyDown(key); }
        public static bool GetKeyDown(string name) { return Instance.GetKeyDown(name); }
        public static bool GetKeyUp(KeyCode key) { return Instance.GetKeyUp(key); }
        public static bool GetKeyUp(string name) { return Instance.GetKeyUp(name); }
        public static bool GetMouseButton(int button) { return Instance.GetMouseButton(button); }
        public static bool GetMouseButtonDown(int button) { return Instance.GetMouseButtonDown(button); }
        public static bool GetMouseButtonUp(int button) { return Instance.GetMouseButtonUp(button); }
        public static Touch GetTouch(int index) { return Instance.GetTouch(index); }
        public static bool IsJoystickPreconfigured(string joystickName) { return Instance.IsJoystickPreconfigured(joystickName); }
        public static void ResetInputAxes() { Instance.ResetInputAxes(); }
    }
}
