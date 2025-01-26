using UnityEngine.Assertions;
using UnityEngine;

public class BubbleMechanicsController : MonoBehaviour
{
    [SerializeField] public float PelletSpawnSpeed = 1f;
    [SerializeField] public GameObject PelletPrefab;

    private GameObject pelletContainer;
    private float lastPelletSpawnTime = 0f;

    void Start() {
        pelletContainer = GameObject.Find("/PelletContainer");
        Assert.IsNotNull(pelletContainer);
    }

    void FixedUpdate() {
        SpawnPellets();
    }

    void SpawnPellets() {
        var now = Time.time;
        if (now - lastPelletSpawnTime > (1 / PelletSpawnSpeed)) {
            lastPelletSpawnTime = now;

            var radius = transform.lossyScale.x * 0.5f;
            var spawnPosition = transform.position + radius * (Vector3)Random.insideUnitCircle;
            Instantiate(
                PelletPrefab,
                spawnPosition,
                Quaternion.identity,
                pelletContainer.transform
            );
        }
    }
}
