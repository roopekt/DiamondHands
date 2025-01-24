using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] public float Speed;

    void Update() {
        if (Input.GetMouseButton(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 movementDirection = Vector3.Normalize(mousePosition - transform.position);

            Vector3 delta = movementDirection * Speed * Time.deltaTime;
            transform.position += delta;
        }
    }
}
