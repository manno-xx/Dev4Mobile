using TMPro;
using UnityEngine;

/// <summary>
/// Just simple display of the vector3 values
/// </summary>
public class ShowAcceleration : MonoBehaviour
{
    [SerializeField] private TMP_Text x;
    [SerializeField] private TMP_Text y;
    [SerializeField] private TMP_Text z;
    
    public void ShowValues(Vector3 acceleration)
    {
        x.text = $"x: {acceleration.x.ToString()}";
        y.text = $"y: {acceleration.y.ToString()}";
        z.text = $"z: {acceleration.z.ToString()}";
    }
}
