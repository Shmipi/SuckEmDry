using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositScript : MonoBehaviour
{

    private float resourceHealth;

    private bool full;
    private bool twoThirds;
    private bool oneThird;
    private bool empty;

    // Start is called before the first frame update
    void Start()
    {
        full = true;
        twoThirds = false;
        oneThird = false;
        empty = false;

        resourceHealth = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if()
    }
}
