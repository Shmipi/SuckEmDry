using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D body;

    private Vector2 moveDirection;

    private Vector2 lookDirection;
    private float lookAngle;

    public bool extracting;

    // Update is called once per frame
    void Update()
    {
        Inputs();

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if (Input.GetKeyDown("e"))
        {
            extracting = true;
            Debug.Log(extracting);
        }

        if (Input.GetKeyUp("e"))
        {
            extracting = false;
            Debug.Log(extracting);
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
}
