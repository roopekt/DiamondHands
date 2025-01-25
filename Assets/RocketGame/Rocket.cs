using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] public float shareValue = 1f;
    [SerializeField] public int sharesOwned = 0;
    [SerializeField] public float gravity = 1f;
    [SerializeField] public float hypeCost = 1f;
    [SerializeField] public float hypeVelocityGain = 1f;

    private float velocity = 0f;
    private bool isActive = false;
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
        transform.position = initialPosition + Vector3.up * shareValue;
    }

    void Update() {
        if (!isActive) return;

        velocity -= gravity * Time.deltaTime;
        shareValue += velocity * Time.deltaTime;
        if (shareValue < 0f) {
            Destroy(gameObject);
        }
        transform.position = initialPosition + Vector3.up * shareValue;
    }

    public bool CanBuy(int shareCount, float cash) {
        return shareValue * shareCount <= cash;
    }

    public float Buy(int shareCount) {
        sharesOwned += shareCount;
        return shareValue * shareCount;
    }

    public bool CanSell(int shareCount) {
        return shareCount <= sharesOwned;
    }

    public float Sell(int shareCount) {
        sharesOwned -= shareCount;
        return shareValue * shareCount;
    }

    public bool CanHype(float cash) {
        return cash >= hypeCost;
    }

    public float Hype() {
        isActive = true;
        velocity += hypeVelocityGain;

        return hypeCost;
    }
}
