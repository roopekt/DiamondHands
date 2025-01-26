using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Rocket : MonoBehaviour
{
    public float shareValue = 1f; //TODO: if changing in editor, fix reseting to reflect new value
    public int sharesOwned = 0;
    public float hypeVelocityGain = 1f;
    public float saleVelocityGain = 1f;
    public float randomVelocityGain = .5f;
    public float hypeCost = 1f;
    public TextMeshProUGUI heightText;

    public  float velocity = 0f;

    public List<string> HypeTweets;
    public List<string> HateTweets;
    public List<AudioClip> HypeAudio;
    public List<AudioClip> HateAudio;
    public TextMeshProUGUI TweetText;
    public TextMeshProUGUI TweetHandle;
    public AudioSource TweetAudioSource;
    private bool lastTweet = false;
    public float TweetDuration = 3f;
    private float lastTweetedTime;
    public Button RestartButton;
    public FinancialAgent Player;
    public RocketFollower VisualRocket;
    
    public float GetVelocity() {
        return velocity;
    }

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
        velocity = Mathf.Clamp(velocity, -15, 15);


        bool tweetCurrent;
        if (Math.Sign(velocity) >= 0) tweetCurrent = true;
        else tweetCurrent = false;
        if (tweetCurrent != lastTweet || Time.time > lastTweetedTime + TweetDuration)
        {
            lastTweet = tweetCurrent;
            lastTweetedTime = Time.time;
            Tweet(tweetCurrent);
        }
    }

    private void Tweet(bool hype)
    {
        if (hype)
        {
            int rng = Random.Range(0, HypeTweets.Count);
            //TweetHandle.text = HypeTweets[rng].Split(":")[0];
            //TweetText.text = HypeTweets[rng].Split(":")[1];
            TweetText.text += "\n" + HypeTweets[rng];
            if (!TweetAudioSource.isPlaying)
            {
                int rng2 = Random.Range(0, HypeAudio.Count);
                //TweetAudioSource.clip = HypeAudio[rng2];
                TweetAudioSource.PlayOneShot(HypeAudio[rng2]);
            }
            
        }
        else
        {
            int rng = Random.Range(0, HateTweets.Count);
            //TweetHandle.text = HateTweets[rng].Split(":")[0];
            //TweetText.text = HateTweets[rng].Split(":")[1];
            TweetText.text += "\n" + HateTweets[rng];
            if (!TweetAudioSource.isPlaying)
            {
                int rng2 = Random.Range(0, HateAudio.Count);
                //TweetAudioSource.clip = HateAudio[rng2];
                TweetAudioSource.PlayOneShot(HateAudio[rng2]);
            }
            
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
        Debug.Log($"Fire random event {evtT} for duration {evtDur}");
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
    private Vector3 resetPosition;

    void Start() {
        initialPosition = transform.position;
        resetPosition = initialPosition;
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
            Player.graph.StopDrawingLine();

            VisualRocket.renderer.enabled = false;
            VisualRocket.explosion?.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
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
        Player.graph.BeginDrawingLine();
    }

    public float GetBuyPrice()
    {
        return (HasRandomEvent(StockEvent.rise) ? 2 : 1f) * shareValue;
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
        return shareValue * shareCount;
    }

    public bool CanSell(int shareCount) {
        if (!isAlive) return false;
        return shareCount <= sharesOwned;
    }

    public float Sell(int shareCount) {
        sharesOwned -= shareCount;
        influence -= saleVelocityGain * (1+ Random.value);
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

    public void Restart()
    {
        RestartButton.gameObject.SetActive(false);
        isActive = false;
        isAlive = true;
        transform.position = resetPosition;
        Player.Reset();
        shareValue = 1;
        sharesOwned = 0;
        VisualRocket.renderer.enabled = true;
        VisualRocket.transform.rotation = Quaternion.Euler(-90, 0, 0);
        Start();
    }
}
