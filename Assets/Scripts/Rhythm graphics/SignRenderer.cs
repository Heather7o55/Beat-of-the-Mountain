using UnityEngine;

public class SignRenderer : MonoBehaviour
{
    private Transform cameratransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameratransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameratransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
