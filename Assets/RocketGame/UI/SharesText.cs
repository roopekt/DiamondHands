using TMPro;
using UnityEngine;

public class SharesText : MonoBehaviour
{
    [SerializeField] public Rocket playerRocket;
    [SerializeField] public TextMeshProUGUI text;

    void Update()
    {
        text.text = Mathf.RoundToInt(playerRocket.sharesOwned).ToString();
    }
}
