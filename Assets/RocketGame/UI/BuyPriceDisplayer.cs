using TMPro;
using UnityEngine;

public class BuyPriceDisplayer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textElement;
    [SerializeField] public Rocket rocket;

    void Update()
    {
        textElement.text = (-rocket.GetBuyPrice()).ToString() + " €";
    }
}
