using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{

    public float gravity;
    public bool fixedDirection;
    public bool secondGravity = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DoubleGravityControl>())
        {
            if(other.GetComponent<DoubleGravityControl>().Gravity1 == null)
            {
                other.GetComponent<DoubleGravityControl>().Gravity1 = this.GetComponent<GravityOrbit>();
            }else if(other.GetComponent<DoubleGravityControl>().Gravity1 != null && other.GetComponent<DoubleGravityControl>().Gravity2 != this.gameObject){
                other.GetComponent<DoubleGravityControl>().Gravity2 = this.GetComponent<GravityOrbit>();
                secondGravity = true;
            }
        }else if(other.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().Gravity = this.GetComponent<GravityOrbit>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<DoubleGravityControl>() &! secondGravity)
        {
            other.GetComponent<DoubleGravityControl>().Gravity1 = null;

        }else if(other.GetComponent<DoubleGravityControl>() && secondGravity)
        {
            other.GetComponent<DoubleGravityControl>().Gravity2 = null;
        }
        else if(other.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().Gravity = null;
        }
    }
}
