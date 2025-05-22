using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class EnhancedSwipe : MonoBehaviour
{
    private TouchInfo started;
    private TouchInfo ended;
    
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count > 0)
        {
            Touch touch = activeTouches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    started = new TouchInfo() { time = Time.time, position = touch.screenPosition };
                    break;
                case TouchPhase.Ended:
                    ended = new TouchInfo() { time = Time.time, position = touch.screenPosition };
                    AnalyseSwipe();
                    break;
                case TouchPhase.Moved:
                    transform.position = Camera.main.ScreenToWorldPoint(touch.screenPosition);
                    break;
            }
        }
    }
    
    /// <summary>
    /// it is a swipe when:
    /// - movement was within a certain time frame
    /// - movement was across a certain distance (converting pixels to inches using DPI)
    /// </summary>
    private void AnalyseSwipe()
    {
        if (ended.time - started.time < .5f && Mathf.Abs(ended.position.x - started.position.x) > .5f * Screen.dpi)
        {
            Debug.Log("Swipe Horizontal!");
        }
    }
}
