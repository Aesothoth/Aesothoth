using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGravityControl : MonoBehaviour
{
    public GravityOrbit Gravity1, Gravity2;
    public ConstantForce force;
    public Rigidbody rb;

    public float rotationSpeed = 20;
    private float distance;
    private Vector3 gravityVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gravity1 || Gravity2)
        {
            Vector3 gravityUp1 = Vector3.zero;
            Vector3 gravityUp2 = Vector3.zero;
            //fixed gravity
            if(Gravity1 && Gravity1.fixedDirection)
            {
                gravityUp1 = Gravity1.transform.up;
            }
            else if(Gravity2 && Gravity2.fixedDirection)
            {
                gravityUp2 = Gravity2.transform.up;
            }
            else if(Gravity1 && Gravity2)
            {
                gravityUp1 = (transform.position - Gravity1.transform.position).normalized;
                gravityUp2 = (transform.position - Gravity2.transform.position).normalized;
            }
            else if(Gravity1 &! Gravity2)
            {
                gravityUp1 = (transform.position - Gravity1.transform.position).normalized;
            }
            else if(Gravity2 &! Gravity1)
            {
                gravityUp2 = (transform.position - Gravity2.transform.position).normalized;
            }


            Vector3 localUp = transform.up;
            Vector3 gravityUp = gravityUp1 + gravityUp2;
            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

            //push down for Gravity;
            if(Gravity1 && Gravity2)
            {
                Vector3 gravityVector1 = new Vector3(-gravityUp1.x * Gravity1.gravity, -gravityUp1.y * Gravity1.gravity, -gravityUp1.z * Gravity1.gravity);
                Vector3 gravityVector2 = new Vector3(-gravityUp2.x * Gravity2.gravity, -gravityUp2.y * Gravity2.gravity, -gravityUp2.z * Gravity2.gravity);
                gravityVector = (gravityVector1 + gravityVector2) / 2;
            }else if(Gravity1 &! Gravity2)
            {
                gravityVector = new Vector3(-gravityUp1.x * Gravity1.gravity, -gravityUp1.y * Gravity1.gravity, -gravityUp1.z * Gravity1.gravity);
            }else if(Gravity2 &! Gravity1)
            {
                gravityVector = new Vector3(-gravityUp2.x * Gravity2.gravity, -gravityUp2.y * Gravity2.gravity, -gravityUp2.z * Gravity2.gravity);
            }


            if(Gravity2 == null)
            {
                distance = Vector3.Distance(transform.position, Gravity1.gameObject.transform.position);
            }else if(Gravity1 == null)
            {
                distance = Vector3.Distance(transform.position, Gravity2.gameObject.transform.position);
            }else{
                distance = Vector3.Distance(transform.position, ((Gravity1.gameObject.transform.position * Gravity1.gravity) + (Gravity2.gameObject.transform.position * Gravity2.gravity) / (Gravity1.gravity + Gravity2.gravity)));
            }
            

            force.force = ((gravityVector * rb.mass) / (distance * distance)) * 10;
            if((Gravity1 && Gravity1.fixedDirection) || (Gravity2 && Gravity2.fixedDirection))
            force.force = (gravityVector * rb.mass);
        }
        else{
            Vector3 gravityVector = new Vector3(0f, -100f, 0f);
            force.force = gravityVector;
        }
    }
}
