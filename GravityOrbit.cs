using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{

    public float gravity;
    public bool fixedDirection;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().Gravity = this.GetComponent<GravityOrbit>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().Gravity = null;
        }
    }

}
