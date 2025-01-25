using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinancialAgent : MonoBehaviour
{
    [SerializeField] public float cash = 2f;
    [SerializeField] public Slider transactionShareCountSilder;
    public StockGraph graph;
    // private int transactionShareCount = 1;

    public float lastSold = 0;
    public float lastBought = 0;

    public float insightPower = .5f;
    public float insightDuration = 3;
    public float insightCooldown = 30;
    float lastInsightUse = 0;
    void Update()
    {
        KeyboardInput();

    }
    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A) )
        {
            Buy();
        }
        if (Input.GetKeyDown(KeyCode.S) )
        {
            Sell();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Hype();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Insight();
        }
    }

    public void Buy() {
        var rocket = GetRocket();
        if (rocket.CanBuy(1, cash)) {
            lastBought = rocket.Buy(1);
            cash -= lastBought;
            graph?.MakePurchase(true);
        }
    }

    public void Sell() {
        var rocket = GetRocket();
        if (rocket.CanSell(1)) {
            lastSold = rocket.Sell(1);
            cash += lastSold;
            graph?.MakePurchase(false);
        }
    }

    public void Hype() {
        var rocket = GetRocket();
        if (rocket.CanHype(cash)) {
            cash -= rocket.Hype();
        }
    }

    public void Insight() {
        if (CanInsight()) {
            StartCoroutine(InsightCoroutine());
            lastInsightUse = Time.time + insightDuration;
        }
    }
    public bool CanInsight()
    {
      return GetRocket().isActive && lastInsightUse < Time.time;
    }
    IEnumerator InsightCoroutine()
    {
        Time.timeScale = insightPower;
        yield return new WaitForSecondsRealtime(insightDuration);
        Time.timeScale = 1;
    }

    Rocket GetRocket() {
        return transform.GetComponentInChildren<Rocket>();
    }
}
