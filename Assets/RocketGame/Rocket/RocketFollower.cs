using UnityEngine;
using UnityEngine.EventSystems;

public class RocketFollower : MonoBehaviour
{
    public Rocket rocketParent;

    private void Update()
    {
        transform.position = rocketParent.transform.position;
        if (rocketParent.isActive)

        transform.rotation =
            Quaternion.FromToRotation(Vector3.up, new Vector3(rocketParent.constantXSpeed, rocketParent.GetVelY(), 0))
            * Quaternion.AngleAxis(-90f, Vector3.right);
        
    }
}
