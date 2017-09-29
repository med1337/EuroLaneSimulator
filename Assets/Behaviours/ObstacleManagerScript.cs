using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{

    GameObject player; //player GO
    private int maxObstacles = 3; //max number of obstacles to spawn
    private int currentObstacles = 0; //how many obstacles spawned
    private int minSpawnTime = 1; //minimum time to next spawn
    private int maxSpawnTime = 5; //maximum time to next spawn
    private float spawnDistance = 10.0f; //distance obstacles spawn/despawn fomr player
    private List<GameObject> obstacles = new List<GameObject>(); //obstacle List
    public GameObject obstacleOne; //obstacle GO to spawn

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("SpawnObstacle", 5);
    }

    // Update is called once per frame
    void Update()
    {
        //if there are obstacles far enough behind player, despawn
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
        //spawn obstacles somewhere in front of player
        if (currentObstacles < maxObstacles)
        {
            Vector3 spawnPoint = player.transform.position;

            spawnPoint.y = spawnPoint.y + spawnDistance;

            float xCoord = Random.Range(-5.0f, 5.0f);

            spawnPoint.x = xCoord;

            currentObstacles++;

            obstacles.Add(Instantiate(obstacleOne, spawnPoint, Quaternion.identity));
        }


        //spawn another in random time range
        int randomSpawn = Random.Range(minSpawnTime, maxSpawnTime);

        Invoke("SpawnObstacle", randomSpawn);
    }
}
