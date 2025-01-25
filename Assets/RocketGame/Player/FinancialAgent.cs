using UnityEngine;
using UnityEngine.UI;

public class FinancialAgent : MonoBehaviour
{
    [SerializeField] public float cash = 2f;
    [SerializeField] public Slider transactionShareCountSilder;
    public StockGraph graph;
    // private int transactionShareCount = 1;

    void Update() {
        //transactionShareCount = (int)transactionShareCountSilder.value;
    }

    public void Buy() {
        var rocket = GetRocket();
        if (rocket.CanBuy(1, cash)) {
            cash -= rocket.Buy(1);
            graph?.MakePurchase(true);
        }
    }

    public void Sell() {
        var rocket = GetRocket();
        if (rocket.CanSell(1)) {
            cash += rocket.Sell(1);
            graph?.MakePurchase(false);
        }
    }

    public void Hype() {
        var rocket = GetRocket();
        if (rocket.CanHype(cash)) {
            cash -= rocket.Hype();
        }
    }

    Rocket GetRocket() {
        return transform.GetChild(0).GetComponent<Rocket>();
    }
}
