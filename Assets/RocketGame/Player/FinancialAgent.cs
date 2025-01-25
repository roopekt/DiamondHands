using UnityEngine;
using UnityEngine.UI;

public class FinancialAgent : MonoBehaviour
{
    [SerializeField] public float cash = 2f;
    [SerializeField] public Slider transactionShareCountSilder;

    // private int transactionShareCount = 1;

    void Update() {
        //transactionShareCount = (int)transactionShareCountSilder.value;
    }

    public void Buy() {
        var rocket = GetRocket();
        if (rocket.CanBuy(1, cash)) {
            cash -= rocket.Buy(1);
        }
    }

    public void Sell() {
        var rocket = GetRocket();
        if (rocket.CanSell(1)) {
            cash += rocket.Sell(1);
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
