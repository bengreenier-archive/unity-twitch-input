using Harmony;
using Harmony.ILCopying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TwitchInput.Core;
using UnityEngine;

namespace TwitchInput.Internal
{
    /// <summary>
    /// Patches <see cref="Input"/>
    /// </summary>
    public class InputPatcher
    {
        /// <summary>
        /// Run the patcher
        /// </summary>
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.bengreenier.unity-twitch-input");

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        #region properties

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("acceleration", PropertyMethod.Getter)]
        public class PatchImpl_acceleration_get
        {
            static bool Prefix(ref Vector3 __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.acceleration;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("accelerationEventCount", PropertyMethod.Getter)]
        public class PatchImpl_accelerationEventCount_get
        {
            static bool Prefix(ref int __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.accelerationEventCount;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("accelerationEvents", PropertyMethod.Getter)]
        public class PatchImpl_accelerationEvents_get
        {
            static bool Prefix(ref AccelerationEvent[] __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.accelerationEvents;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("anyKey", PropertyMethod.Getter)]
        public class PatchImpl_anyKey_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.anyKey;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("anyKeyDown", PropertyMethod.Getter)]
        public class PatchImpl_anyKeyDown_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.anyKeyDown;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("backButtonLeavesApp", PropertyMethod.Getter)]
        public class PatchImpl_backButtonLeavesApp_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.backButtonLeavesApp;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("backButtonLeavesApp", PropertyMethod.Setter)]
        public class PatchImpl_backButtonLeavesApp_set
        {
            static bool Prefix(bool value)
            {
                try
                {
                    // set original result
                    TwInput.backButtonLeavesApp = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compass", PropertyMethod.Getter)]
        public class PatchImpl_compass_get
        {
            static bool Prefix(ref Compass __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.compass;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compensateSensors", PropertyMethod.Getter)]
        public class PatchImpl_compensateSensors_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.compensateSensors;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compensateSensors", PropertyMethod.Setter)]
        public class PatchImpl_compensateSensors_set
        {
            static bool Prefix(bool value)
            {
                try
                {
                    // set original result
                    TwInput.compensateSensors = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compositionCursorPos", PropertyMethod.Getter)]
        public class PatchImpl_compositionCursorPos_get
        {
            static bool Prefix(ref Vector2 __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.compositionCursorPos;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compositionCursorPos", PropertyMethod.Setter)]
        public class PatchImpl_compositionCursorPos_set
        {
            static bool Prefix(Vector2 value)
            {
                try
                {
                    // set original result
                    TwInput.compositionCursorPos = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("compositionString", PropertyMethod.Getter)]
        public class PatchImpl_compositionString_get
        {
            static bool Prefix(ref string __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.compositionString;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("deviceOrientation", PropertyMethod.Getter)]
        public class PatchImpl_deviceOrientation_get
        {
            static bool Prefix(ref DeviceOrientation __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.deviceOrientation;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("eatKeyPressOnTextFieldFocus", PropertyMethod.Getter)]
        public class PatchImpl_eatKeyPressOnTextFieldFocus_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.eatKeyPressOnTextFieldFocus;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("eatKeyPressOnTextFieldFocus", PropertyMethod.Setter)]
        public class PatchImpl_eatKeyPressOnTextFieldFocus_set
        {
            static bool Prefix(bool value)
            {
                try
                {
                    // set original result
                    TwInput.eatKeyPressOnTextFieldFocus = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("gyro", PropertyMethod.Getter)]
        public class PatchImpl_gyro_get
        {
            static bool Prefix(ref Gyroscope __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.gyro;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("imeCompositionMode", PropertyMethod.Getter)]
        public class PatchImpl_imeCompositionMode_get
        {
            static bool Prefix(ref IMECompositionMode __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.imeCompositionMode;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("imeCompositionMode", PropertyMethod.Setter)]
        public class PatchImpl_imeCompositionMode_set
        {
            static bool Prefix(IMECompositionMode value)
            {
                try
                {
                    // set original result
                    TwInput.imeCompositionMode = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("imeIsSelected", PropertyMethod.Getter)]
        public class PatchImpl_imeIsSelected_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.imeIsSelected;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("inputString", PropertyMethod.Getter)]
        public class PatchImpl_inputString_get
        {
            static bool Prefix(ref string __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.inputString;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("isGyroAvailable", PropertyMethod.Getter)]
        public class PatchImpl_isGyroAvailable_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.isGyroAvailable;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("location", PropertyMethod.Getter)]
        public class PatchImpl_location_get
        {
            static bool Prefix(ref LocationService __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.location;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("mousePosition", PropertyMethod.Getter)]
        public class PatchImpl_mousePosition_get
        {
            static bool Prefix(ref Vector3 __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.mousePosition;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("mousePresent", PropertyMethod.Getter)]
        public class PatchImpl_mousePresent_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.mousePresent;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("mouseScrollDelta", PropertyMethod.Getter)]
        public class PatchImpl_mouseScrollDelta_get
        {
            static bool Prefix(ref Vector2 __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.mouseScrollDelta;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("multiTouchEnabled", PropertyMethod.Getter)]
        public class PatchImpl_multiTouchEnabled_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.multiTouchEnabled;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("multiTouchEnabled", PropertyMethod.Setter)]
        public class PatchImpl_multiTouchEnabled_set
        {
            static bool Prefix(bool value)
            {
                try
                {
                    // set original result
                    TwInput.multiTouchEnabled = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("simulateMouseWithTouches", PropertyMethod.Getter)]
        public class PatchImpl_simulateMouseWithTouches_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.simulateMouseWithTouches;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("simulateMouseWithTouches", PropertyMethod.Setter)]
        public class PatchImpl_simulateMouseWithTouches_set
        {
            static bool Prefix(bool value)
            {
                try
                {
                    // set original result
                    TwInput.simulateMouseWithTouches = value;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("stylusTouchSupported", PropertyMethod.Getter)]
        public class PatchImpl_stylusTouchSupported_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.stylusTouchSupported;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("touchCount", PropertyMethod.Getter)]
        public class PatchImpl_touchCount_get
        {
            static bool Prefix(ref int __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.touchCount;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("touches", PropertyMethod.Getter)]
        public class PatchImpl_touches_get
        {
            static bool Prefix(ref Touch[] __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.touches;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("touchPressureSupported", PropertyMethod.Getter)]
        public class PatchImpl_touchPressureSupported_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.touchPressureSupported;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("touchSupported", PropertyMethod.Getter)]
        public class PatchImpl_touchSupported_get
        {
            static bool Prefix(ref bool __result)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.touchSupported;
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }

                // don't execute original
                return false;
            }
        }

        #endregion

        #region methods

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetAxis")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetAxis
        {
            static bool Prefix(ref float __result, string axisName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetAxis(axisName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetAxisRaw")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetAxisRaw
        {
            static bool Prefix(ref float __result, string axisName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetAxisRaw(axisName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetButton")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetButton
        {
            static bool Prefix(ref bool __result, string buttonName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetButton(buttonName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetButtonDown")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetButtonDown
        {
            static bool Prefix(ref bool __result, string buttonName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetButtonDown(buttonName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetButtonUp")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetButtonUp
        {
            static bool Prefix(ref bool __result, string buttonName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetButtonUp(buttonName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetJoystickNames")]
        public class PatchImpl_GetJoystickNames
        {
            static bool Prefix(ref string[] __result, string buttonName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetJoystickNames();
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetKey")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetKey
        {
            static bool Prefix(ref bool __result, string name)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetKey(name);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetKeyDown")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetKeyDown
        {
            static bool Prefix(ref bool __result, KeyCode key)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetKeyDown(key);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetKeyDown")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetKeyDown_2
        {
            static bool Prefix(ref bool __result, string name)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetKeyDown(name);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetKeyUp")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetKeyUp
        {
            static bool Prefix(ref bool __result, KeyCode key)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetKeyUp(key);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetKeyUp")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_GetKeyUp_2
        {
            static bool Prefix(ref bool __result, string name)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetKeyUp(name);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetMouseButton")]
        [HarmonyPatch(new Type[] { typeof(int) })]
        public class PatchImpl_GetMouseButton
        {
            static bool Prefix(ref bool __result, int button)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetMouseButton(button);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetMouseButtonDown")]
        [HarmonyPatch(new Type[] { typeof(int) })]
        public class PatchImpl_GetMouseButtonDown
        {
            static bool Prefix(ref bool __result, int button)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetMouseButtonDown(button);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetMouseButtonUp")]
        [HarmonyPatch(new Type[] { typeof(int) })]
        public class PatchImpl_GetMouseButtonUp
        {
            static bool Prefix(ref bool __result, int button)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetMouseButtonUp(button);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("GetTouch")]
        [HarmonyPatch(new Type[] { typeof(int) })]
        public class PatchImpl_GetTouch
        {
            static bool Prefix(ref Touch __result, int index)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.GetTouch(index);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("IsJoystickPreconfigured")]
        [HarmonyPatch(new Type[] { typeof(string) })]
        public class PatchImpl_IsJoystickPreconfigured
        {
            static bool Prefix(ref bool __result, string joystickName)
            {
                try
                {
                    // set/patch original result
                    __result = TwInput.IsJoystickPreconfigured(joystickName);
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        [HarmonyPatch(typeof(Input))]
        [HarmonyPatch("ResetInputAxes")]
        public class PatchImpl_ResetInputAxes
        {
            static bool Prefix()
            {
                try
                {
                    TwInput.ResetInputAxes();
                }
                catch (NotImplementedException)
                {
                    // execute original method
                    return true;
                }
                
                // don't execute original
                return false;
            }
        }

        #endregion
    }
}
