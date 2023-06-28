using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    public Vector3 orientation;
    // Start is called before the first frame update
    void Start()
    {
        orientation = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
