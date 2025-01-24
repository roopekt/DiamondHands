using UnityEngine;

public class PelletCrowdingDestroyer : MonoBehaviour
{
    public float timeOfSpawn { get; private set; }

    void Start() {
        timeOfSpawn = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        PelletCrowdingDestroyer otherCrowdingDestroyer = other.GetComponent<PelletCrowdingDestroyer>();
        Debug.Log("collision");
        if (otherCrowdingDestroyer != null && timeOfSpawn > otherCrowdingDestroyer.timeOfSpawn) {
            Debug.Log("destroy");
            Destroy(transform.parent.gameObject);
        }
    }
}
