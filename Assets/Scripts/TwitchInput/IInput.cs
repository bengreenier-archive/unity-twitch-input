using UnityEngine;

/// <summary>
/// Interface derived from <see cref="Input"/> statics implementation
/// </summary>
public interface IInput
{
    Vector3 acceleration { get; }
    int accelerationEventCount { get; }
    AccelerationEvent[] accelerationEvents { get; }
    bool anyKey { get; }
    bool anyKeyDown { get; }
    bool backButtonLeavesApp { get; set; }
    Compass compass { get; }
    bool compensateSensors { get; set; }
    Vector2 compositionCursorPos { get; set; }
    string compositionString { get; }
    DeviceOrientation deviceOrientation { get; }
    bool eatKeyPressOnTextFieldFocus { get; set; }
    Gyroscope gyro { get; }
    IMECompositionMode imeCompositionMode { get; set; }
    bool imeIsSelected { get; }
    string inputString { get; }
    bool isGyroAvailable { get; }
    LocationService location { get; }
    Vector3 mousePosition { get; }
    bool mousePresent { get; }
    Vector2 mouseScrollDelta { get; }
    bool multiTouchEnabled { get; set; }
    bool simulateMouseWithTouches { get; set; }
    bool stylusTouchSupported { get; }
    int touchCount { get; }
    Touch[] touches { get; }
    bool touchPressureSupported { get; }
    bool touchSupported { get; }
    AccelerationEvent GetAccelerationEvent(int index);
    float GetAxis(string axisName);
    float GetAxisRaw(string axisName);
    bool GetButton(string buttonName);
    bool GetButtonDown(string buttonName);
    bool GetButtonUp(string buttonName);
    string[] GetJoystickNames();
    bool GetKey(string name);
    bool GetKeyDown(KeyCode key);
    bool GetKeyDown(string name);
    bool GetKeyUp(KeyCode key);
    bool GetKeyUp(string name);
    bool GetMouseButton(int button);
    bool GetMouseButtonDown(int button);
    bool GetMouseButtonUp(int button);
    Touch GetTouch(int index);
    bool IsJoystickPreconfigured(string joystickName);
    void ResetInputAxes();
}
