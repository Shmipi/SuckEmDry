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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip laserShot;

    public HealthBar healthBar;
    private GCScript gc;

    private Vector2 moveDirection;

    private Vector2 lookDirection;
    private float lookAngle;

    public bool extracting;

    [SerializeField] GameObject laserGun;
    [SerializeField] GameObject backGun;
    [SerializeField] GameObject leftGun;
    [SerializeField] GameObject rightGun;
    [SerializeField] GameObject laser;

    public bool secretActive;

    private void Start()
    {
        secretActive = false;

        moveSpeed = 10;

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
        if(isAlive == true && PauseMenuScript.GameIsPaused == false)
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
                gc.DestructionFx(2);
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
        body.velocity = new Vector2(moveDirection.x * (moveSpeed + gc.speedIncrease), moveDirection.y * (moveSpeed + gc.speedIncrease));
    }

    private void FireBullet()
    {
        if(secretActive == false)
        {
            GameObject firedLaser = Instantiate(laser, laserGun.GetComponent<Transform>().position, laserGun.GetComponent<Transform>().rotation);
            firedLaser.GetComponent<Rigidbody2D>().velocity = laserGun.GetComponent<Transform>().up * 25f;
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(laserShot);
        } else
        {
            //Front
            GameObject firedLaser = Instantiate(laser, laserGun.GetComponent<Transform>().position, laserGun.GetComponent<Transform>().rotation);
            firedLaser.GetComponent<Rigidbody2D>().velocity = laserGun.GetComponent<Transform>().up * 25f;

            //Back
            GameObject firedBackLaser = Instantiate(laser, backGun.GetComponent<Transform>().position, backGun.GetComponent<Transform>().rotation);
            firedBackLaser.GetComponent<Rigidbody2D>().velocity = backGun.GetComponent<Transform>().up * 25f;

            //Left
            GameObject firedLeftLaser = Instantiate(laser, leftGun.GetComponent<Transform>().position, leftGun.GetComponent<Transform>().rotation);
            firedLeftLaser.GetComponent<Rigidbody2D>().velocity = leftGun.GetComponent<Transform>().up * 25f;

            //Right
            GameObject firedRightLaser = Instantiate(laser, rightGun.GetComponent<Transform>().position, rightGun.GetComponent<Transform>().rotation);
            firedRightLaser.GetComponent<Rigidbody2D>().velocity = rightGun.GetComponent<Transform>().up * 25f;
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(laserShot);
        }
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
