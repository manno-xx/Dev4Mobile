using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField, Range(0, 200)] private float maxHealth;

    public UnityEvent<float> healthUpdated;
    
    void Start()
    {
        health = maxHealth;
        healthUpdated.Invoke(health/maxHealth);
    }

    private void OnMouseDown()
    {
        DoDamage(10);
    }
    
    public void DoDamage(float amount)
    {
        health -= amount;
        healthUpdated.Invoke(health/maxHealth);
    }
}
