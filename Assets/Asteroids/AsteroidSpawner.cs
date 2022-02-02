using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public Asteroid asteroid;

    public float spawnRate = 2.0f;

    public float spawnDistance = 15.0f;

    public int spawnAmount = 1;

    public float startSpeed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {

        //InvokeRepeating("Spawn", 2.0f, 0.5f);

        StartCoroutine("Spawn");

    }



    IEnumerator Spawn()
    {

        for ( int j = 0; j < 3; j++ )
        {

            for (int i = 0; i <= spawnAmount; i++)
            {

                Vector3 direction = Random.insideUnitCircle.normalized * this.spawnDistance;

                var spawnPoint = this.transform.position + direction;

                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Asteroid Asteroid = Instantiate(asteroid, spawnPoint, rotation);

                Rigidbody2D rb = Asteroid.GetComponent<Rigidbody2D>();

                rb.velocity = -Asteroid.transform.position.normalized * startSpeed;

            }

            yield return new WaitForSeconds(2.0f);

        }



    }



}
