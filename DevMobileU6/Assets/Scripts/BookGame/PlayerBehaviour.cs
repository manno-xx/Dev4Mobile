using Lean.Touch;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and receiving input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody _rb;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0,10)]
    public float rollSpeed = 5;

    private float _steeringForce;
    
    private Camera _mainCamera;
    //private bool _isFingerTouching = false;
    
    /// <summary>
    /// Prep the object
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }
    
    /// <summary>
    /// Do the physics update
    /// if there is input (received using LeanTouch, see below), the steeringForce will be applied
    /// When the finger is released off the screen, the steeringForce is set to zero (zee below)
    /// </summary>
    void FixedUpdate()
    {
        _rb.AddForce(_steeringForce, 0, rollSpeed, ForceMode.Force);
    }
    
    /// <summary>
    /// Receives info on where the player touches the screen
    /// </summary>
    /// <param name="finger"></param>
    public void LeanOnFinger(LeanFinger finger)
    {
        var viewportPos = _mainCamera.ScreenToViewportPoint(finger.ScreenPosition);
        
        // do the math to make position in the viewport also influence the _size_ of the force (not only direction)
        _steeringForce = (viewportPos.x * 2 - 1) * dodgeSpeed;
    }
    
    /// <summary>
    /// For when the finger is pressed, set a flag / boolean to indicate the finger is on the screen
    /// </summary>
    /// <param name="finger"></param>
    public void OnFingerDown(LeanFinger finger)
    {
        //_isFingerTouching = true;
    }
    
    /// <summary>
    /// For when the finger is released, set a flag / boolean to indicate the finger is off the screen
    /// </summary>
    /// <param name="finger"></param>
    public void OnFingerUp(LeanFinger finger)
    {
        //_isFingerTouching = false;
        _steeringForce = 0;
    }
    
    /// <summary>
    /// Just for debugging: show that velocity as a Gizmo line
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // if(EditorApplication.isPlaying || EditorApplication.isPaused)
        //     Gizmos.DrawLine(transform.position, transform.position + _rb.linearVelocity);
    }
}
