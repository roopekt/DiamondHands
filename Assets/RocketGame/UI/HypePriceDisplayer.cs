using TMPro;
using UnityEngine;

public class HypePriceDisplayer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textElement;
    [SerializeField] public Rocket rocket;

    void Update()
    {
        textElement.text = (-rocket.GetHypePrice()).ToString("F2") + " €";
    }
}
