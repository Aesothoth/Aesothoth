using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbit : MonoBehaviour
{
    public Rigidbody rb;
    public GravityOrbit gravity;
    public float speed;
    public bool binaryOrbit;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartOrbit", 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void StartOrbit()
    {

        moveDirection = transform.forward;
        rb.AddForce(moveDirection * speed, ForceMode.Impulse);

    }
}
