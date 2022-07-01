using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    private float maxHealth = 1000f;
    private float resourceHealth;
    public HealthBar healthBar;
    public GameObject healthBarRender;

    private bool twoThirds;
    private bool oneThird;
    public bool empty;

    private bool isColliding;

    private GameObject player;
    private PlayerMovement pm;

    private GCScript gc;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite first;
    [SerializeField] private Sprite second;
    [SerializeField] private Sprite third;
    [SerializeField] private Sprite fourth;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = first;

        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GCScript>();

        twoThirds = false;
        oneThird = false;
        empty = false;

        resourceHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarRender.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceHealth > 666)
        {
            twoThirds = false;
            oneThird = false;
            empty = false;
            spriteRenderer.sprite = first;
        } else if(resourceHealth <= 666 && resourceHealth > 333)
        {
            spriteRenderer.sprite = second;

            if(twoThirds == false)
            {
                gc.RespawnDepletion();
                twoThirds = true;
            }
        } else if(resourceHealth <= 333 && resourceHealth > 0)
        {
            spriteRenderer.sprite = third;

            if(oneThird == false)
            {
                gc.RespawnDepletion();
                oneThird = true;
            }
        } else if(resourceHealth <= 0)
        {
            spriteRenderer.sprite = fourth;

            if(empty == false)
            {
                gc.RespawnDepletion();
                empty = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(isColliding == true && pm.extracting == true && resourceHealth > 0)
        {
            healthBar.enabled = true;
            resourceHealth -= 1f;
            healthBar.SetHealth(resourceHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isColliding = true;
            healthBarRender.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isColliding = false;
            healthBarRender.SetActive(false);
        }
    }
}
