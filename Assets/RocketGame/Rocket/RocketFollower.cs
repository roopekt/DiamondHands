using UnityEngine;
using UnityEngine.EventSystems;

public class RocketFollower : MonoBehaviour
{
    public Rocket rocketParent;

    private void Update()
    {
        transform.position = rocketParent.transform.position;
        if (rocketParent.isActive)
        transform.up = new Vector3(rocketParent.constantXSpeed, rocketParent.GetVelY(), 0);
    }
}
