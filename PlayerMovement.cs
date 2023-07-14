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
    public float jumpForce;
    public float airMultiplier;
    private bool launchKey1, launchKey2, jumpKey;
    [HideInInspector]
    public bool firstLaunch, secondLaunch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    void Update()
    {
        launchKey1 = Input.GetKeyDown("q");
        launchKey2 = Input.GetKeyDown("e");
        jumpKey = Input.GetKeyDown("space");
        grounded = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.5f + 0.2f, ground);

        if(grounded)
        {
            rb.drag = groundDrag;
            firstLaunch = false;
            secondLaunch = false;
        }
        else{
            rb.drag = 0;
        }
        if(launchKey1)
       {
            if(!firstLaunch)
            {
                Launch(1);
                firstLaunch = true;
            }else if(!secondLaunch)
            {
                Launch(1);
                secondLaunch = true;
            }
       }else if(launchKey2)
       {
            if(!firstLaunch)
            {
                Launch(-1);
                firstLaunch = true;
            }else if(!secondLaunch)
            {
                Launch(-1);
                secondLaunch = true;
            }
       }
       if(jumpKey)
       {
            if(grounded)
            {
                Jump();
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
        moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);

        if(grounded)
        {
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }else{
            rb.AddForce(moveDirection * moveSpeed * airMultiplier, ForceMode.Force);
        }
       
    }

    private void Launch(float num)
    {
        // reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 up = new Vector3(0f, num, 0f);

        rb.AddForce(up * launchForce, ForceMode.Impulse);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
