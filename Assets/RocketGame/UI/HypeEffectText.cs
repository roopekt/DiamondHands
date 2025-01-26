using TMPro;
using UnityEngine;

public class HypeEffectText : MonoBehaviour
{
    [SerializeField] public FinancialAgent agentToTrack;
    [SerializeField] public TextMeshProUGUI text;

    void Update()
    {
        SetVisibility(agentToTrack.cash > 20f);
        text.text = agentToTrack.cash.ToString("F2");
    }

    void SetVisibility(bool isVisible) {
        if (text.enabled != isVisible) {
            text.enabled = isVisible;
        }
    }
}
