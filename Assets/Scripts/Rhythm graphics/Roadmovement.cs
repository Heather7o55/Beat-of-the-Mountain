using UnityEngine;

public class Roadmovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("spawned");
        rb.linearVelocity = new Vector3(0,0,1) * movementSpeed;
    }
}
