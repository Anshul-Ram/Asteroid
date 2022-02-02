using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{


    public Sprite[] sprites;

    public Asteroid asteroid;

    public float size = 1.0f;

    public float minSize = 0.75f;

    public float maxSize = 1.5f;

    public float startSpeed = 2.0f;

    public float minPosSize = 0.5f;

    private SpriteRenderer sprite;

    private Rigidbody2D rb;

    private Vector3 up = new Vector3( 0f, 1f, 0f);

    private bool enteredSpace = false;


    private void Awake()
    {

        sprite = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        this.size = Random.Range( minSize, maxSize );

    }

    // Update is called once per frame
    private void Start()
    {

        sprite.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        this.transform.localScale = Vector3.one * this.size;

        rb.mass = this.size;

    }


    private void Update()
    {

        var y = transform.position.y;
        var x = transform.position.x;

        // Finding out if the asteroid entered the visible space.
        if ( Mathf.Abs(y) < 5 & Mathf.Abs(x) < 9 )
        {
            enteredSpace = true;

        }


        // Teleport to the other side when the player crosses a boundary.
        if ( (Mathf.Abs(y) > 5 || Mathf.Abs(x) > 9) & enteredSpace )
        {

            teleport(x, y);

        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // Detect a collision with a bullet.
        if ( collision.gameObject.layer == 7 )
        {

            scatter();

        }

    }

    private void scatter()
    {


        if ( this.size / 2 < minPosSize )
        {

            Destroy(gameObject);
            return;

        }


        var euler = Vector3.Angle( rb.velocity, up );

        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        Asteroid Smalleroid = Instantiate(asteroid, this.transform.position, rotation);

        Smalleroid.size = this.size / 2;

        Rigidbody2D Small_rb = Smalleroid.GetComponent<Rigidbody2D>();

        //rotation = Quaternion.Euler( 0f, 0f, Random.Range( euler, euler + 60f ) );

        rotation = Quaternion.AngleAxis( Random.Range( euler, euler + 60f ), new Vector3(0f, 0f , 1f) );

        up = new Vector3( 0f, 1f, 0f );

        Small_rb.velocity = rotation * up * startSpeed;



        rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        Smalleroid = Instantiate(asteroid, this.transform.position, rotation);

        Smalleroid.size = this.size / 2;

        Small_rb = Smalleroid.GetComponent<Rigidbody2D>();

        //rotation = Quaternion.Euler(0f, 0f, Random.Range(euler, euler - 60f));

        rotation = Quaternion.AngleAxis(Random.Range(euler, euler - 60f), new Vector3(0f, 0f, 1f));

        Small_rb.velocity = rotation * up * startSpeed;

        Destroy(gameObject);

    }


    private void teleport( float x, float y )
    {

        if (y > 5)
        {

            transform.position += new Vector3(0f, -10f, 0f);

        }
        else if (y < -5)
        {

            transform.position += new Vector3(0f, 10f, 0f);

        }

        if (x > 9)
        {

            transform.position += new Vector3(-18f, 0f, 0f);

        }
        else if (x < -9)
        {

            transform.position += new Vector3(18f, 0f, 0f);

        }

    }



}
