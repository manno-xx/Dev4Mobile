using Lean.Touch;
using UnityEngine;


public class MoveOnDelta : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    
    public void FingerDeltaUpdate(Vector2 delta)
    {
        var normalized = delta.normalized;
        transform.Translate(normalized * speed);
    }

    public void FingerUpdate(LeanFinger finger)
    {
        var normalized = (finger.ScreenPosition - finger.StartScreenPosition).normalized;
        transform.Translate(normalized * speed * Time.deltaTime);
    }
}
