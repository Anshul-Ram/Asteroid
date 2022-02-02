using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 4;

    public float Timer;

    public float invincibilityTimer = 2.0f;

    public GameObject healthSprite;

    private PolygonCollider2D polyCollider;

    private SpriteRenderer sprite;

    private GameObject HealthOverlay;

    private GameObject DeathScreen;

    private Text timeSurvived;


    // Start is called before the first frame update
    void Start()
    {

        Timer = invincibilityTimer;

        polyCollider = GetComponent<PolygonCollider2D>();

        sprite = GetComponent<SpriteRenderer>();

        HealthOverlay = GameObject.Find("HealthOverlay");

        timeSurvived = GameObject.Find("Time Survived").GetComponent<Text>();

        DeathScreen = GameObject.Find("DeathScreen");

        DeathScreen.SetActive(false);

        // Add sprites to the health bar.
        for (int i = 0; i < Health; i++)
        {

            var healthBar = Instantiate(healthSprite);

            healthBar.transform.position = HealthOverlay.transform.position + new Vector3( ( Health - i - 1 ) * 0.8f, 0f, 0f );

            healthBar.transform.parent = HealthOverlay.transform;


        }


    }

    // Update is called once per frame
    void Update()
    {

        Timer += Time.deltaTime;


        // The player dies.
        if ( Health <= 0 )
        {

            //polyCollider.enabled = false;

            //sprite.color = new Color(1f, 1f, 1f, 0f);

            timeSurvived.text = "You survived: " + Mathf.Round(Time.time) + " seconds!";

            DeathScreen.SetActive(true);

            Destroy(gameObject);


        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ( collision.gameObject.layer == 9 )
        {

            asteroidHit();

        }

    }


    private void asteroidHit( )
    {

        if ( Timer >= invincibilityTimer)
        {

            Health -= 1;

            Timer = 0;

            healthSprite = HealthOverlay.transform.GetChild(0).gameObject;

            Destroy(healthSprite);

            StartCoroutine( flashPlayer() );

        }

    }


    IEnumerator flashPlayer()
    {


        for ( int i = 0; i < 8; i++ )
        {

            sprite.color = new Color(1f, 1f, 1f, 0.1f);


            yield return new WaitForSeconds(0.1f);


            sprite.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(0.1f);

        }


    }


}
