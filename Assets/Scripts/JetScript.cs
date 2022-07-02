using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetScript : MonoBehaviour
{
    private float maxHealth = 5f;
    private float health;

    [SerializeField] private float speed = 9f;
    private GameObject player;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip rocketShot;

    private GCScript gc;

    private Vector2 lookDirection;
    private float lookAngle;

    private bool inRange;
    private bool rocketRange;
    private bool movingTowards;

    private GameObject target;

    private float startTime = 0f;
    private float holdTime = 0.2f;
    private float timer = 0f;

    private bool canShoot;
    private bool shotOne;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject bulletSpawn1;
    [SerializeField] private GameObject bulletSpawn2;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        shotOne = false;

        health = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GCScript>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreLayerCollision(7, 9);
        Physics2D.IgnoreLayerCollision(9, 10);

        movingTowards = true;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("jetTarget");
        int targetIndex = Random.Range(0, 4);

        target = targets[targetIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().isAlive == true)
        {
            var step = speed * Time.deltaTime;

            if (movingTowards == true)
            {
                lookDirection = player.GetComponent<Transform>().position - transform.position;
                lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

                transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, step);

                if (Mathf.Abs(Vector3.Distance(player.GetComponent<Transform>().position, transform.position)) < 9)
                {
                    inRange = true;
                }
                else
                {
                    inRange = false;
                }
            }
            else
            {
                lookDirection = target.GetComponent<Transform>().position - transform.position;
                lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

                transform.position = Vector3.MoveTowards(transform.position, target.GetComponent<Transform>().position, step);

                inRange = false;
            }

            if (Mathf.Abs(Vector3.Distance(player.GetComponent<Transform>().position, transform.position)) <= 5)
            {
                rocketRange = true;
                holdTime = 0.5f;
            }
            else
            {
                rocketRange = false;
                holdTime = 0.2f;
            }

            if (Mathf.Abs(Vector3.Distance(player.GetComponent<Transform>().position, transform.position)) <= 2)
            {
                movingTowards = false;
            }

            if (Mathf.Abs(Vector3.Distance(target.GetComponent<Transform>().position, transform.position)) <= 1)
            {
                movingTowards = true;
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
            gc.IncreaseXp(5);
            gc.DestructionFx(1, gameObject.transform);
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        if (inRange == true && canShoot == true)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        if(shotOne == false)
        {
            if(rocketRange == false)
            {
                GameObject firedBullet = Instantiate(bullet, bulletSpawn1.GetComponent<Transform>().position, bulletSpawn1.GetComponent<Transform>().rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn1.GetComponent<Transform>().up * 25f;
                audioSource.pitch = Random.Range(0.6f, 1.0f);
                audioSource.PlayOneShot(shot);
            } else
            {
                GameObject firedBullet = Instantiate(rocket, bulletSpawn1.GetComponent<Transform>().position, bulletSpawn1.GetComponent<Transform>().rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn1.GetComponent<Transform>().up * 20f;
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(rocketShot);
            }

            canShoot = false;
            shotOne = true;
            startTime = Time.time;
            timer = startTime;

        } else
        {
            if (rocketRange == false)
            {
                GameObject firedBullet = Instantiate(bullet, bulletSpawn2.GetComponent<Transform>().position, bulletSpawn2.GetComponent<Transform>().rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn2.GetComponent<Transform>().up * 25f;
                audioSource.pitch = Random.Range(0.6f, 1.0f);
                audioSource.PlayOneShot(shot);
            }
            else
            {
                GameObject firedBullet = Instantiate(rocket, bulletSpawn2.GetComponent<Transform>().position, bulletSpawn2.GetComponent<Transform>().rotation);
                firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn2.GetComponent<Transform>().up * 20f;
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(rocketShot);
            }

            canShoot = false;
            shotOne = false;
            startTime = Time.time;
            timer = startTime;
        }
    }

    public void TakeDamage()
    {
        health -= 1 * gc.damageMultiplier;
    }
}
