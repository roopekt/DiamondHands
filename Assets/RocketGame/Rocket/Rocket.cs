using TMPro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float shareValue = 1f;
    public int sharesOwned = 0;
    public float hypeVelocityGain = 1f;
    public float saleVelocityGain = 1f;
    public float randomVelocityGain = .5f;
    public float hypeCost = 1f;
    public TextMeshProUGUI heightText;

   public  float velocity = 0f;
    void AdjustVelocity()
    {
        if (HasRandomEvent(StockEvent.crash) && shareValue > 10)
        {
            velocity = Random.Range(.2f, -1f) * Mathf.Abs(InfluenceVel());
        }
        else
        if (HasRandomEvent(StockEvent.rise) )
        {
            velocity = Random.Range(0, 2f) * Mathf.Abs(InfluenceVel());
        }
        else
        {
            velocity = (Random.Range(-.8f, 1f) ) * InfluenceVel();
        if (HasRandomEvent(StockEvent.chaos))
            velocity *= Random.value * 2;
        }
    }
    float InfluenceVel()
    {
        float final = .5f * Mathf.Sign(influence) + influence + ((playerBoostDuration > Time.time) ? playerBoostInfluence : 0);
        return final ;
    }

    float influence = 0;


    public float constantXSpeed = 1f;

    float playerBoostInfluence;
    float playerBoostDuration;

    public float randomEventChance = .33f;
    public float randomEventInterfal = 3;
    public float randomEventDuration = 4;
    float nextRandomEvent = 0;
    void CheckRandomEvent()
    {
        if (nextRandomEvent < Time.time)
        {
            if (Random.value < randomEventChance) {
                RandomEvent();
            }
            else
            {
                nextRandomEvent = Time.time  + randomEventInterfal;
            }
        }
    }
    void RandomEvent()
    {
        FireEvent((StockEvent)Random.Range(0, (int)StockEvent.total), randomEventDuration);
    }
    void FireEvent(StockEvent evtT, float evtDur)
    {
        Debug.Log($"Fire randon event {evtT} for duration {evtDur}");
        randomEvent = evtT;
        randomEventDuraiton = Time.time + evtDur;
        nextRandomEvent = Time.time + evtDur + randomEventInterfal;
        if (evtT == StockEvent.inflluencedump)
        {
            if (influence > 10)
                influence *= .5f;
            influence -= Random.value * 3;
        }
    }
    public enum StockEvent
    {
        crash,
        rise,
        chaos,
        inflluencedump,
        total,
    }
    StockEvent randomEvent;
    float randomEventDuraiton;

    bool HasRandomEvent(StockEvent eventType)
    {
        return randomEventDuraiton > Time.time && eventType == randomEvent;
    }

    float adjustVelocityTime = 3;
    public bool isActive = false;
    public bool isAlive = true;
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
        transform.position = initialPosition + Vector3.up * shareValue;
    }

    void Update()
    {
        if (!isAlive) return;

        if (heightText != null)
        {
            heightText.text = shareValue.ToString("F2") + " €";
        }
        hypeCost = GetHypeCost();
        if (!isActive) return;

        CheckRandomEvent();
        if (adjustVelocityTime < Time.time)
        {
            AdjustVelocity();
            adjustVelocityTime = Time.time + (HasRandomEvent(StockEvent.chaos) ? Random.value : 1) *.5f;
        }
        shareValue += velocity * Time.deltaTime;
        if (shareValue < 0f) {
            isAlive = false;
            return;
        }
        initialPosition += Vector3.right * constantXSpeed * Time.deltaTime ;
        transform.position = initialPosition + Vector3.up * shareValue ;

    }
    public void Launch()
    {
        if (isActive) return;
        AdjustVelocity();
        isActive = true;
        FireEvent(StockEvent.rise, 1);
    }

    public float GetBuyPrice()
    {
        return (HasRandomEvent(StockEvent.rise) ? .5f : 1f) * shareValue;
    }
    public float GetSellPrice()
    {
        return (HasRandomEvent(StockEvent.chaos) ? .5f : 1f) * shareValue;
    }

    public bool CanBuy(int shareCount, float cash) {
        if (!isAlive) return false;
        return shareValue * shareCount <= cash;
    }

    public float Buy(int shareCount) {
        sharesOwned += shareCount;
        influence += saleVelocityGain * 2 * Random.value ;
        influence = Mathf.Clamp(influence, -10, 10);
        return shareValue * shareCount;
    }

    public bool CanSell(int shareCount) {
        if (!isAlive) return false;
        return shareCount <= sharesOwned;
    }

    public float Sell(int shareCount) {
        sharesOwned -= shareCount;
        influence -= saleVelocityGain * (1+ Random.value);
        influence = Mathf.Clamp(influence, -10, 10);
        return GetSellPrice() * shareCount;
    }
    public float GetHypeCost()
    {
        return !isActive ? 0 : (.5f + shareValue / 2);
    }

    public bool CanHype(float cash) {
        if (!isAlive) return false;
        return cash >= GetHypeCost()
;
    }

    public float Hype() {
        float cost = GetHypeCost();
        playerBoostInfluence = hypeVelocityGain;
        playerBoostDuration = Mathf.Max(Time.time + 3, playerBoostDuration + 3);

        Launch();
        return cost;
    }
}
