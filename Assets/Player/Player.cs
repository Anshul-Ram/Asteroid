using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public Bullet bulletPrefab;

    public float turnSpeed = 1.0f;

    public float thrustSpeed = 1.0f;

    private GameObject oppBoundary;

    private Rigidbody2D rb;

    private bool thrusting;

    private float turn;


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

    }



    private void Update()
    {


        thrusting = ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {

            turn = 1.0f;

        } else if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {

            turn = -1.0f;

        } else
        {

            turn = 0.0f;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            Shoot();

        }


        var y = transform.position.y;
        var x = transform.position.x;

        // Teleport to the other side when the player crosses a boundary.
        if ( Mathf.Abs(y) > 5 || Mathf.Abs(x) > 9 )
        {

            teleport(x, y);

        }

    }


    private void FixedUpdate()
    {
        
        if (thrusting)
        {
            rb.AddForce(this.transform.up * this.thrustSpeed);
        }

        if ( turn != 0.0f )
        {

            rb.AddTorque( turn * this.turnSpeed );

        }


    }


    private void teleport( float x, float y)
    {

        if ( y > 5 )
        {

            transform.position += new Vector3(0f, -10f, 0f);

        } else if ( y < -5 )
        {

            transform.position += new Vector3(0f, 10f, 0f);

        }

        if ( x > 9 )
        {

            transform.position += new Vector3(-18f, 0f, 0f);

        } else if ( x < -9)
        {

            transform.position += new Vector3(18f, 0f, 0f);

        }

    }


    private void Shoot()
    {

        Bullet bullet = Instantiate( this.bulletPrefab, this.transform.position, this.transform.rotation);

        bullet.Project( this.transform.up );

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ( collision.gameObject.layer == 8 )
        {

            string[] words = collision.gameObject.name.Split(' ');

            boundaryCollision( words[0] );

        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="boundary"></param>
    private void boundaryCollision( string boundary )
    {

        Debug.Log( boundary );

        if ( boundary == "Top" )
        {

            oppBoundary = GameObject.Find("Bottom Boundary");

            var y = 2 * oppBoundary.transform.position.y + oppBoundary.transform.localScale.y + 0.75f * this.transform.localScale.x;

            this.transform.position += new Vector3( 0f, y, 0f);


        } else if ( boundary == "Bottom" )
        {

            oppBoundary = GameObject.Find("Top Boundary");

            var y = 2 * oppBoundary.transform.position.y - oppBoundary.transform.localScale.y - 0.75f * this.transform.localScale.y;

            this.transform.position += new Vector3(0f, y, 0f);

        } else if ( boundary == "Left" )
        {

            oppBoundary = GameObject.Find("Right Boundary");

            var x = 2 * oppBoundary.transform.position.x - oppBoundary.transform.localScale.y - 0.75f * this.transform.localScale.x;

            this.transform.position += new Vector3( x, 0f, 0f);


        } else if ( boundary == "Right" )
        {

            oppBoundary = GameObject.Find("Left Boundary");

            var x = 2 * oppBoundary.transform.position.x + oppBoundary.transform.localScale.y + 0.75f * this.transform.localScale.x;

            this.transform.position += new Vector3( x, 0f, 0f);

        }

    }

}
