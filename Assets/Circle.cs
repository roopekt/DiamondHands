using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = Vector3.right * Mathf.Sin(Time.time);
    }
}
