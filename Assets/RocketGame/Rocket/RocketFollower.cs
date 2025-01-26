using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RocketFollower : MonoBehaviour
{
    public Rocket rocketParent;
    public Renderer renderer;
    public ParticleSystem explosion;

    Quaternion desiredRotation;
    private void Update()
    {
        transform.position = rocketParent.transform.position;
        if (rocketParent.isActive)
        {

            desiredRotation =
            Quaternion.FromToRotation(Vector3.up, new Vector3(rocketParent.constantXSpeed, rocketParent.GetVelocity(), 0))
            * Quaternion.AngleAxis(-90f, Vector3.right);
            if (transform.rotation != desiredRotation && rotationCoroutine == null)
            {
                rotationCoroutine = StartCoroutine(Rotate(transform.rotation, desiredRotation, .333f));
            }
        }
    }
    Coroutine rotationCoroutine;
    IEnumerator Rotate(Quaternion start, Quaternion end, float dur)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / dur)
        {
            transform.rotation = Quaternion.Lerp(start, end, t);
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = end;
        rotationCoroutine = null;

    }
}
