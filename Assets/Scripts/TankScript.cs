using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float speed = 5f;
    private GameObject player;

    private Vector2 lookDirection;
    private float lookAngle;

    private bool inRange;

    [SerializeField] private GameObject tankShell;
    [SerializeField] private GameObject bulletSpawn;

    private float startTime = 0f;
    [SerializeField] private float holdTime = 2f;
    private float timer = 0f;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        lookDirection = player.GetComponent<Transform>().position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if (Mathf.Abs(Vector3.Distance(player.GetComponent<Transform>().position, transform.position)) > 4)
        {
            animator.SetBool("isMoving", true);
            inRange = false;
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, step);
        }
        else
        {
            animator.SetBool("isMoving", false);
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

    private void FixedUpdate()
    {
        if (inRange == true && canShoot == true)
        {
            fireBullet();
        }
    }

    private void fireBullet()
    {
        GameObject firedBullet = Instantiate(tankShell, bulletSpawn.GetComponent<Transform>().position, bulletSpawn.GetComponent<Transform>().rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawn.GetComponent<Transform>().up * 15f;
        canShoot = false;
        startTime = Time.time;
        timer = startTime;
    }
}