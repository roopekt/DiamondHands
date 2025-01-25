using UnityEngine;

public class RocketAudioController : MonoBehaviour
{   
    public Rocket rocket;
    public AudioSource thrusterAudioSource;
    public AudioClip thrusterClip;
    public AudioClip sputteringClip;
    public float sputteringVelocity = 1f;
    public float velocityForMaxThrusterPower = 10f;

    private ThrusterState thrusterState = ThrusterState.Off;

    enum ThrusterState {
        On,
        Off,
        Sputtering
    }

    void Update()
    {
        UpdateThrusterAudio();
    }

    void UpdateThrusterAudio() {
        var velocity = rocket.GetVelY();

        var newState = ThrusterState.Off;
        if (velocity > 0f) newState = ThrusterState.Sputtering;
        if (velocity > sputteringVelocity) newState = ThrusterState.On;

        if (newState != thrusterState) {
            thrusterState = newState;
            if (thrusterState == ThrusterState.Off) {
                thrusterAudioSource.Stop();
            }
            if (thrusterState == ThrusterState.Sputtering) {
                thrusterAudioSource.clip = sputteringClip;
                thrusterAudioSource.Play();
            }
            if (thrusterState == ThrusterState.On) {
                thrusterAudioSource.clip = thrusterClip;
                thrusterAudioSource.Play();
            }
        }

        if (thrusterState == ThrusterState.On) {
            thrusterAudioSource.volume = Mathf.Clamp(velocity / velocityForMaxThrusterPower, 0f, 1f);
        }
        else {
            thrusterAudioSource.volume = 1f;
        }
    }
}
