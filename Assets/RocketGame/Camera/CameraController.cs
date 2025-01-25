using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Max(target.position.x, 0),
            Mathf.Max(target.position.y, 0),
            transform.position.z);
    }
}
