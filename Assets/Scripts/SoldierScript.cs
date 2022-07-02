using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shot;

    [SerializeField] private float speed = 2.5f;
    private GameObject player;
    private GCScript gc;

    private float health;

    private Vector2 lookDirection;
    private float lookAngle;

    private bool inRange;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawn;

    private float startTime = 0f;
    [SerializeField] private float holdTime = 0.5f;
    private float timer = 0f;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        health = 1f;

        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GCScript>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().isAlive == true)
        {
            lookDirection = player.GetComponent<Transform>().position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

            if (Mathf.Abs(Vector3.Distance(player.GetComponent<Transform>().position, transform.position)) > 2)
            {
                animator.SetBool("isWalking", true);
                inRange = false;
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, step);
            }
            else
            {
                animator.SetBool("isWalking", false);
                inRange = true;
            }

            if (canShoot == false)
            {
                timer += Time.deltaTime;
                if (timer > (startTime + holdTime))
                {
                    canShoot = true;
                }
            }
        }

        if(health <= 0)
        {
            gc.IncreaseXp(1);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(inRange == true && canShoot == true)
        {
            fireBullet();
        }
    }

    private void fireBullet()
    {
        GameObject firedBullet = Instantiate(bullet, bulletSpawn.GetComponent<Transform>().position, bulletSpawn.GetComponent<Transform>().rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn.GetComponent<Transform>().up * 10f;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(shot);
        canShoot = false;
        startTime = Time.time;
        timer = startTime;
    }

    public void TakeDamage()
    {
        health -= 1 * gc.damageMultiplier;
    }
}
