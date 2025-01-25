using UnityEngine;

public class FishCon : MonoBehaviour
{
    public float maximumRotationSpeed = 3;
    public float maximumVelocity = 3;

    public float sprintAngle = 1;
    public float turnSpeed = .35f;

    public Rigidbody2D rigidbody;
    Vector2 moveDirection;

    private void Update()
    {
        moveDirection =  Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
    }
    public void FixedUpdate()
    {
        float lerpedAngle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) % 360;
        float absoluteMaxRot = maximumRotationSpeed;

        float deltaAngle = Mathf.DeltaAngle( rigidbody.rotation, lerpedAngle);

        deltaAngle = Mathf.Clamp(deltaAngle, -absoluteMaxRot, absoluteMaxRot);

        float velMult = Mathf.Abs(deltaAngle) < sprintAngle ? 1 : turnSpeed;
        rigidbody.linearVelocity = transform.right * velMult * maximumVelocity;
        rigidbody.rotation += deltaAngle;
    }
}
