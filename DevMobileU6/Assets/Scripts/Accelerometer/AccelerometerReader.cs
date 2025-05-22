using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Script that reads the accelerometer and sends a Unity Event with the data _every_ frame
/// It has options to smooth the data using a simple low pass filter
///   (apply smoothing or not and the strength of the smoothing)
/// This script requires an input action called "Accelerometer" (reading the accelerometer)
/// </summary>
public class AccelerometerReader : MonoBehaviour
{
    private InputAction _accelerometerAction; 
    
    private Vector3 rawAcceleration;
    private Vector3 smoothAcceleration;
    
    [SerializeField, Tooltip("The value to change the smoothing. 0 is max smoothing, 1 is no smoothing"), Range(0, 1)] 
    private float _smoothingParameter;

    [SerializeField, Tooltip("Do or do not smooth the data")]
    private bool doSmoothData;
    
    public UnityEvent<Vector3> AccelerometerUpdatEvent;

    protected void OnEnable()
    {
        _accelerometerAction = InputSystem.actions.FindAction("Accelerometer");
    }
    
    /// <summary>
    /// Read the accelerometer smooth the data and send out an event 
    /// </summary>
    protected void Update()
    {
        rawAcceleration = _accelerometerAction.ReadValue<Vector3>();
        
        if(doSmoothData)
            smoothAcceleration = SmoothValue(rawAcceleration);
        else
            smoothAcceleration = rawAcceleration;
        
        AccelerometerUpdatEvent.Invoke(smoothAcceleration);
    }
    
    /// <summary>
    /// Does a basic Low pass smoothing on the accelerometer data.
    /// </summary>
    /// <param name="rawValue">The raw values from the accelerometer</param>
    /// <returns>The smoothed data in a Vector3</returns>
    private Vector3 SmoothValue(Vector3 rawValue)
    {
        Vector3 output = Vector3.zero;
        output.x = _smoothingParameter * rawValue.x + (1 - _smoothingParameter) * smoothAcceleration.x;
        output.y = _smoothingParameter * rawValue.y + (1 - _smoothingParameter) * smoothAcceleration.y;
        output.z = _smoothingParameter * rawValue.z + (1 - _smoothingParameter) * smoothAcceleration.z;
        return output;
    }
}
