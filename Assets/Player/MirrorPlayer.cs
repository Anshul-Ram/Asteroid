using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayer : MonoBehaviour
{

    public Bullet bulletPrefab;

    public float turnSpeed = 1.0f;

    public float thrustSpeed = 1.0f;

    private Rigidbody2D rb;

    private bool thrusting;

    private float turn;



    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {

        thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {

            turn = 1.0f;

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            turn = -1.0f;

        }
        else
        {

            turn = 0.0f;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            Shoot();

        }

    }


    private void FixedUpdate()
    {

        if (thrusting)
        {

            rb.AddForce(this.transform.up * this.thrustSpeed);

        }

        if (turn != 0.0f)
        {

            rb.AddTorque(turn * this.turnSpeed);

        }


    }


    private void Shoot()
    {

        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);

        bullet.Project(this.transform.up);

    }



}
