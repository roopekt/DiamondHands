using TMPro;
using UnityEngine;

public class CashText : MonoBehaviour
{
    [SerializeField] public FinancialAgent agentToTrack;
    [SerializeField] public TextMeshProUGUI text;

    void Update()
    {
        text.text = agentToTrack.cash.ToString("F2") + " €";
    }
}
