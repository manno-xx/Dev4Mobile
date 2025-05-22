using UnityEngine;

/// <summary>
/// Add force to a game object based onb accelerometer data
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ForceOnAcceleration : MonoBehaviour
{
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnAccelerometerData(Vector3 accelerometerValue)
    {
        rb.AddForce(accelerometerValue.x, 0, accelerometerValue.y, ForceMode.Force);
    }
}
