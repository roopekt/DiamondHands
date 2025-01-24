using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    public float Speed;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition3D = new Vector3(mousePosition2D.x, mousePosition2D.y, 0f);
            Vector3 movementDirection = Vector3.Normalize(mousePosition3D - transform.position);

            Vector3 delta = movementDirection * Speed * Time.deltaTime;
            transform.position += delta;
        }
    }
}
