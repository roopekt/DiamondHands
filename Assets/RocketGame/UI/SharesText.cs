using TMPro;
using UnityEngine;

public class SharesText : MonoBehaviour
{
    [SerializeField] public Rocket playerRocket;
    [SerializeField] public TextMeshProUGUI text;

    void Update()
    {
        text.text = playerRocket.sharesOwned.ToString("F2");
    }
}
