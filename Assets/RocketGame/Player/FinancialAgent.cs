using System;
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

    private float initlastSold = 0;
    private float initlastBought = 0;

    private float initinsightPower = .5f;
    private float initinsightDuration = 3;
    private float initinsightCooldown = 30;
    private float initlastInsightUse = 0;

    private float initCash;
    

    private void Start()
    {
        //Save Variables
        initlastSold = lastSold;
        initlastBought = lastBought;

        initinsightPower = insightPower;
        initinsightDuration = insightDuration;
        initinsightCooldown = insightCooldown;
        initlastInsightUse = lastInsightUse;
        
        initCash = cash;
    }

    void Update()
    {
        KeyboardInput();

    }
    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A) )
        {
            Buy(Input.GetKey(KeyCode.LeftShift) ? 5  : 1);
        }
        if (Input.GetKeyDown(KeyCode.S) )
        {
            Sell(Input.GetKey(KeyCode.LeftShift) ? 5 : 1);
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

    public void Buy(int amt = 1) {
        var rocket = GetRocket();
        if (rocket.CanBuy(amt, cash)) {
            lastBought = rocket.Buy(amt);
            cash -= lastBought;
            graph?.MakePurchase(true);
        }
    }

    public void Sell(int amt = 1) {
        var rocket = GetRocket();
        if (rocket.CanSell(amt)) {
            lastSold = rocket.Sell(amt);
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

    public void Reset()
    {
        lastSold = initlastSold;
        lastBought = initlastBought;
        insightPower = initinsightPower;
        insightDuration = initinsightDuration;
        insightCooldown = initinsightCooldown;
        lastInsightUse = initlastInsightUse;
        
        cash = initCash;
    }
}
