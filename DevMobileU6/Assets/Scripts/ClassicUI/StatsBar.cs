using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private Gradient barColor;
    
    public void UpdateBar(float newValue)
    {
        image.fillAmount = newValue;
        image.color = barColor.Evaluate(newValue);
    }
}
