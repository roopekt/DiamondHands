using TMPro;
using UnityEngine;

public class SellPriceDisplayer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textElement;
    [SerializeField] public Rocket rocket;

    void Update()
    {
        textElement.text = rocket.GetSellPrice().ToString() + " €";
    }
}
