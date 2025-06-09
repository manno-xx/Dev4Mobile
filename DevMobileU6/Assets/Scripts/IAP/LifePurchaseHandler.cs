using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

/// <summary>
/// the flow around purchasing a life
/// </summary>
public class LifePurchaseHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    
    public void LifeProductFetchedHandler(Product product)
    {
        // price will be 0 (zero) bc it is not defined in the store
        label.text = $"{product.metadata.localizedTitle} €({product.metadata.localizedPrice})";
    }

    public void LifeProductPurchasedHandler(Product product)
    {
        Debug.Log($"Congrats, you just bought {product.definition.payout.quantity} of {product.metadata.localizedTitle}");
    }
}
