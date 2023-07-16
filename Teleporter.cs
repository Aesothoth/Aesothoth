using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    private bool reset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        reset = Input.GetKeyDown("r");
        if(reset)
        {
            player.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
