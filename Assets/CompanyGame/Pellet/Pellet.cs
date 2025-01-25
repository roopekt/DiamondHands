using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] public float MaxInitialSpeed = 1f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Random.insideUnitCircle * MaxInitialSpeed;
    }
}
