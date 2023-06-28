using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;

    public Transform orientation;
    Rigidbody rb;

    float horizontal;
    float vertical;

    Vector3 moveDirection;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public float groundDrag;
    public bool grounded;

    [Header("Vertical Launch")]
    public float launchForce;
    public float airMultiplier;
    bool firstLaunch, secondLaunch, launchKey;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    void Update()
    {
        launchKey = Input.GetKeyDown("space");
        grounded = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.5f + 0.2f, ground);

        if(grounded)
        {
            firstLaunch = false;
            secondLaunch = false;
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
        if(launchKey)
       {
            if(grounded)
            {
                Launch();
            }else
            {
                if(!firstLaunch)
                {
                    Launch();
                    firstLaunch = true;
                }else if(!secondLaunch)
                {
                    Launch();
                    secondLaunch = true;
                }
            }
       }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MyInput();
        MovePlayer();
    }

    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        if(grounded)
        {
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }else{
            rb.AddForce(moveDirection * moveSpeed * airMultiplier, ForceMode.Force);
        }
       
    }

    private void Launch()
    {
        // reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * launchForce, ForceMode.Impulse);
        print("Jump");
    }
}
