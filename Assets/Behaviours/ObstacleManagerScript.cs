using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{

    GameObject player;
    private int maxObstacles = 3;
    private int currentObstacles = 0;
    private int minSpawnTime = 1;
    private int maxSpawnTime = 5;
    private float spawnDistance = 10.0f;
    private List<GameObject> obstacles = new List<GameObject>();
    public GameObject obstacleOne;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("SpawnObstacle", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObstacles > 0)
        {
            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.transform.position.y > player.transform.position.y - spawnDistance)
                {
                    obstacles.Remove(obstacle);

                    Destroy(obstacle);

                    currentObstacles--;                    
                }
            }
        }
    }

    void SpawnObstacle()
    {
        if (currentObstacles < maxObstacles)
        {
            Vector3 spawnPoint = player.transform.position;

            spawnPoint.y = spawnPoint.y + spawnDistance;

            float xCoord = Random.Range(-5.0f, 5.0f);

            spawnPoint.x = xCoord;

            //Instantiate(obstacleOne, spawnPoint, Quaternion.identity);

            currentObstacles++;

            obstacles.Add(Instantiate(obstacleOne, spawnPoint, Quaternion.identity));
        }

        int randomSpawn = Random.Range(minSpawnTime, maxSpawnTime);

        Invoke("SpawnObstacle", randomSpawn);
    }
}
