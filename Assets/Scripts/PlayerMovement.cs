using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float maxHealth = 1000f;
    public float health;
    public bool isAlive;

    public HealthBar healthBar;
    private GCScript gc;

    private Vector2 moveDirection;

    private Vector2 lookDirection;
    private float lookAngle;

    public bool extracting;

    [SerializeField] GameObject laserGun;
    [SerializeField] GameObject laser;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        animator.enabled = true;
        spriteRenderer.enabled = true;

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GCScript>();

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == true)
        {
            Inputs();

            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

            if (Input.GetKeyDown("e"))
            {
                extracting = true;
                animator.SetBool("isHarvesting", true);
                Debug.Log(extracting);
            }

            if (Input.GetKeyUp("e"))
            {
                extracting = false;
                animator.SetBool("isHarvesting", false);
                Debug.Log(extracting);
            }

            if (Input.GetMouseButtonDown(0) && extracting == false)
            {
                FireBullet();
            }

            if (health <= 0)
            {
                animator.enabled = false;
                spriteRenderer.enabled = false;
                isAlive = false;
                gc.GameOver();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        body.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void FireBullet()
    {
        GameObject firedLaser = Instantiate(laser, laserGun.GetComponent<Transform>().position, laserGun.GetComponent<Transform>().rotation);
        firedLaser.GetComponent<Rigidbody2D>().velocity = laserGun.GetComponent<Transform>().up * 25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            health -= 5;
            healthBar.SetHealth(health);
        } else if(collision.gameObject.tag == "Rocket")
        {
            health -= 10;
            healthBar.SetHealth(health);
        }
    }
}
