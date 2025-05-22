using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Store relevant data of a touch
/// - time
/// - position
/// </summary>
public struct TouchInfo
{
    public float time;
    public Vector3 position;

    public override string ToString()
    {
        return $"position {position}, time: {time}";
    }
}

/// <summary>
/// Testing touches in new Input system and (Unity Remote)
/// Bit of a random test.
/// In the end, it positions the transform of this game object.
/// Which accidentally has a Trail Renderer component (well, actually because we can)
/// </summary>
public class Swiper : MonoBehaviour
{
    private InputAction swipe;

    private TouchInfo _touchBegin;
    private TouchInfo _touchPerformed;
    private TouchInfo _touchCanceled;
    
    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// sets the position of this transform based on the touch performed's info (position)
    /// </summary>
    private void Update()
    {
        var pos = _touchPerformed.position;
        pos.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    /// <summary>
    /// Initializes the touch stuff.
    /// See the InputSystem.inputsettings Asset in the project for definition of 'PrimaryTouch'
    /// </summary>
    void Initialize()
    {
        swipe = InputSystem.actions.FindAction("PrimaryTouch");
        swipe.started += TouchStart;
        swipe.performed += TouchPerformed;
        swipe.canceled += TouchCanceled;
        
        Debug.Log("Initialized!");
    }
    
    /// <summary>
    /// Store touch info when a touch starts
    /// </summary>
    /// <param name="obj"></param>
    private void TouchStart(InputAction.CallbackContext obj)
    {
        _touchBegin = new TouchInfo()
        {
            time = Time.time,
            position = obj.ReadValue<Vector2>()
        };
        Debug.Log($"started: {_touchBegin}");
    }

    /// <summary>
    /// Store touch info when a touch is 'performed'
    /// </summary>
    /// <param name="obj"></param>
    private void TouchPerformed(InputAction.CallbackContext obj)
    {
        _touchPerformed = new TouchInfo()
        {
            time = Time.time,
            position = obj.ReadValue<Vector2>()
        };
        Debug.Log($"performed: {_touchPerformed}");
    }
    
    
    /// <summary>
    /// Store touch info when a touch is cencelled
    /// </summary>
    /// <param name="obj"></param>
    private void TouchCanceled(InputAction.CallbackContext obj)
    {
        _touchCanceled = new TouchInfo()
        {
            time = Time.time,
            position = obj.ReadValue<Vector2>()
        };
        Debug.Log($"canceled: {_touchCanceled}");
    }
}
