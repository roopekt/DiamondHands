using UnityEngine;
using UnityEngine.UI;

public class FinancialAgent : MonoBehaviour
{
    [SerializeField] public float cash = 2f;
    [SerializeField] public Slider transactionShareCountSilder;

    private int transactionShareCount = 1;

    void Update() {
        transactionShareCount = (int)transactionShareCountSilder.value;
    }

    public void Buy() {
        var rocket = transform.GetChild(0).GetComponent<Rocket>();
        if (rocket.CanBuy(transactionShareCount, cash)) {
            cash -= rocket.Buy(transactionShareCount);
        }
    }

    public void Sell() {
        var rocket = transform.GetChild(0).GetComponent<Rocket>();
        if (rocket.CanSell(transactionShareCount)) {
            cash += rocket.Sell(transactionShareCount);
        }
    }
}
