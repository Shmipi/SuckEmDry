using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    private float resourceHealth;

    private bool full;
    private bool twoThirds;
    private bool oneThird;
    private bool empty;

    private bool isColliding;

    private GameObject player;
    private PlayerMovement pm;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();

        full = true;
        twoThirds = false;
        oneThird = false;
        empty = false;

        resourceHealth = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceHealth > 666)
        {
            full = true;
            twoThirds = false;
            oneThird = false;
            empty = false;
            spriteRenderer.color = Color.blue;
        } else if(resourceHealth <= 666 && resourceHealth > 333)
        {
            full = false;
            twoThirds = true;
            oneThird = false;
            empty = false;
            spriteRenderer.color = Color.red;
        } else if(resourceHealth <= 333 && resourceHealth > 0)
        {
            full = false;
            twoThirds = false;
            oneThird = true;
            empty = false;
            spriteRenderer.color = Color.green;
        } else if(resourceHealth <= 0)
        {
            full = false;
            twoThirds = false;
            oneThird = false;
            empty = true;
            spriteRenderer.color = Color.black;
        }
    }

    private void FixedUpdate()
    {
        if(isColliding == true && pm.extracting == true)
        {
            resourceHealth -= 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}
