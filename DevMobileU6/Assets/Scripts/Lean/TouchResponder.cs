using Lean.Touch;
using UnityEngine;

/// <summary>
/// Responds to LeanTouch event to position itself
/// The new position is the hit point of a ray cast from the finger's position 
/// </summary>
public class TouchResponder : MonoBehaviour
{
    /// <summary>
    /// Receives info from LeanTouch's OnFinger Event
    /// afaik, that event triggers every frame that a finger is on the screen.
    /// It sends information on where the finger is etc.
    /// For what the information is, see https://carloswilkes.com/Documentation/LeanTouch#LeanFinger
    /// </summary>
    /// <param name="finger">The object holding the info on the finger on the screen</param>
    public void OnLeanFinger(LeanFinger finger)
    {
        // just show the screen position (where the finger is on the screen in pixels) in the console
        Debug.Log(finger.ScreenPosition);
        // cast a ray from where the finger is into the world
        if(Physics.Raycast(finger.GetRay(), out RaycastHit hitInfo))
        {
            // if the ray has hit anything, place this game object at that point of impact
            transform.position = hitInfo.point;            
        }
    }
}
