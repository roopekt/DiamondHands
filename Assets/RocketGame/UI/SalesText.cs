using TMPro;
using UnityEngine;

public class SalesText : MonoBehaviour
{
    public FinancialAgent agentToTrack;
    public TextMeshProUGUI sellText;
    public TextMeshProUGUI buyText;
    void Update()
    {
        sellText.text = agentToTrack.lastSold > 0 ? (agentToTrack.lastSold + " €") : ""; ;
        buyText.text = agentToTrack.lastBought > 0 ? (agentToTrack.lastBought + " €") : ""; ;
    }
}
