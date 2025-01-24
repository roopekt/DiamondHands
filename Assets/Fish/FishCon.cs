using UnityEngine;

public class FishCon : MonoBehaviour
{ 
    public float maximumRotationSpeed = 3;
    public float maximumVelocity = 3;

    public float a = 1;
    public float b = .35f;

    public float lerpedAngle;
    public float deltaAngle;
    
    public Rigidbody2D rigidbody;
    Vector2 moveDirection;

    private void Update()
    {
        moveDirection = transform.position -  Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void FixedUpdate()
    {
         lerpedAngle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) % 360;
                float absoluteMaxRot = maximumRotationSpeed ;

         deltaAngle = Mathf.DeltaAngle( lerpedAngle, rigidbody.rotation);

        deltaAngle = Mathf.Clamp(deltaAngle, -absoluteMaxRot, absoluteMaxRot);

        float velMult = Mathf.Abs(deltaAngle) > a ? 1 : b;
        rigidbody.linearVelocity = transform.right * velMult * maximumVelocity;
        rigidbody.angularVelocity = deltaAngle;
    }
}
