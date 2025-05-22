using Lean.Touch;
using UnityEngine;

//
public class DragMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private Vector3 targetPosition;
    private Vector3 _normalized;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// recieves the data of the swipe in world space coordinates
    /// </summary>
    /// <param name="swipe"></param>
    public void OnWorldDelta(Vector3 swipe)
    {
        Debug.Log($"Swipe was: {swipe}");
        _normalized = swipe.normalized;
        
        targetPosition = transform.position + _normalized * speed;
    }

    /// <summary>
    /// Move the rigid body in the physics update
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime));
    }
}
