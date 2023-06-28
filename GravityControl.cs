using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public GravityOrbit Gravity;
    public ConstantForce force;
    public Rigidbody rb;

    public float rotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;
            //fixed gravity
            if(Gravity.fixedDirection)
            {
                gravityUp = Gravity.transform.up;
            }else{
                gravityUp = (transform.position - Gravity.transform.position).normalized;
            }
            Vector3 localUp = transform.up;
            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

            //push down for Gravity;
            Vector3 gravityVector = new Vector3(-gravityUp.x * Gravity.gravity, -gravityUp.y * Gravity.gravity, -gravityUp.z * Gravity.gravity);
            float distance = Vector3.Distance(transform.position, Gravity.gameObject.transform.position);
            if(!Gravity.fixedDirection)
            force.force = ((gravityVector * rb.mass) / (distance * distance)) * 10;
            else
            force.force = (gravityVector * rb.mass);
        }
        else{
            Vector3 gravityVector = new Vector3(0f, 0f, 0f);
            force.force = gravityVector;
        }
    }
}
