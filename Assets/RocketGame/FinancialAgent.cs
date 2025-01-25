using UnityEngine;

public class FinancialAgent : MonoBehaviour
{
    [SerializeField] public float cash = 2f;

    public void Buy() {
        var rocket = transform.GetChild(0).GetComponent<Rocket>();
        if (rocket.CanBuy(1, cash)) {
            cash -= rocket.Buy(1);
        }
    }

    public void Sell() {
        var rocket = transform.GetChild(0).GetComponent<Rocket>();
        if (rocket.CanSell(1)) {
            cash += rocket.Sell(1);
        }
    }
}
