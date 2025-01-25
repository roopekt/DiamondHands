using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
     public float shareValue = 1f;
     public int sharesOwned = 0;
     public float gravity = 1f;
     public float hypeVelocityGain = 1f;
     public float saleVelocityGain = 1f;
     public float randomVelocityGain = .5f;
     public float constantXSpeed = 1f;
     public TextMeshProUGUI heightText;

    private float velocity = 0f;
    float randomVelocity = 0;
    float lastRandomVelocityTime = 3;
    private bool isActive = false;
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
        transform.position = initialPosition + Vector3.up * shareValue;
    }

    void Update()
    {
        if (heightText != null)
        {
            heightText.text = shareValue.ToString("F2") + " €";
        }
        if (!isActive) return;

        velocity -= gravity * Time.deltaTime;
        if (lastRandomVelocityTime < Time.time)
        {
            randomVelocity = transform.position.y < 10 ? 0 : Random.Range(-1, 1) * (velocity * randomVelocityGain);
            lastRandomVelocityTime = Time.time + 3 *  Random.value;
        }
        shareValue += (velocity + randomVelocity ) * Time.deltaTime;
        if (shareValue < 0f) {
            Destroy(gameObject);
            return;
        }
        initialPosition += Vector3.right * constantXSpeed * Time.deltaTime ;
        transform.position = initialPosition + Vector3.up * shareValue ;

    }

    public float GetBuyPrice() {
        return shareValue;
    }
    public float GetSellPrice() {
        return shareValue;
    }

    public bool CanBuy(int shareCount, float cash) {
        return shareValue * shareCount <= cash;
    }

    public float Buy(int shareCount) {
        sharesOwned += shareCount;
        velocity += saleVelocityGain * Random.value;
        return shareValue * shareCount;
    }

    public bool CanSell(int shareCount) {
        return shareCount <= sharesOwned;
    }

    public float Sell(int shareCount) {
        sharesOwned -= shareCount;
        velocity -= saleVelocityGain * Random.value;
        return shareValue * shareCount;
    }
    public float GetHypeCost()
    {
        return !isActive ? 0 : shareValue * 2;
    }

    public bool CanHype(float cash) {
        return cash >= GetHypeCost()
;
    }

    public float Hype() {
        isActive = true;
        velocity += hypeVelocityGain;

        return GetHypeCost();
    }
}
