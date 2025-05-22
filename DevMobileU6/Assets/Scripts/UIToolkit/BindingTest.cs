using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Class that demonstrates binding of its variables to a UIDocument's visual elements
/// In this case it binds the health float to the value of a progress bar element
/// From https://en.wikipedia.org/wiki/Data_binding:
///  > "In a data binding process, each data change is reflected automatically by the elements that are bound to the data"
/// </summary>
public class BindingTest : MonoBehaviour
{
    [SerializeField] private float health;
    
    void Start()
    {
        SetupHealthBinding();
    }

    private void OnMouseDown()
    {
        DoDamage(10);
    }
    
    /// <summary>
    /// Apply a certain amount of damage to the health
    /// </summary>
    /// <param name="amount"></param>
    private void DoDamage(int amount)
    {
        health -= amount;
    }

    /// <summary>
    /// Set up the binding between health and the <ProgressBar>("ProgressBar-DB-script")
    /// </summary>
    private void SetupHealthBinding()
    {
        UIDocument doc = FindFirstObjectByType<UIDocument>();
        var root = doc.rootVisualElement;
        root.dataSource = this;
        
        ProgressBar bar = root.Q<ProgressBar>("ProgressBar-DB-script");
        bar.SetBinding("value", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(BindingTest.health))
        });
    }
}
