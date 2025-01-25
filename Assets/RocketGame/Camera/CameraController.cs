using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Max(target.position.x, 0),
            transform.position.y,
            transform.position.z);
    }
}
