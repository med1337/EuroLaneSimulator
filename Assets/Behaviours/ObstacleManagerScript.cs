using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    GameObject player; //player GO
    private int minSpawnTime = 3; //minimum time to next spawn
    private int maxSpawnTime = 6; //maximum time to next spawn
    private int spawnDistance = 50; //distance from player to spawn a car
    public List<GameObject> obstacles = new List<GameObject>(); //obstacle List
    public GameObject car; //car GO to spawn

    public bool car_spawn_right = false;
    public bool car_spawn_left = false;
    public bool car_spawn_up = false;
    public bool car_spawn_down = false;

    private bool thing_queued;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < spawnDistance)
        {
            if (!thing_queued)
            {
                int randomSpawn = Random.Range(minSpawnTime, maxSpawnTime);

                Invoke("SpawnObstacle", randomSpawn);

                thing_queued = true;
            }
        }
    }

    void SpawnObstacle()
    {
        thing_queued = false;

        Vector3 spawnPoint = transform.position;

        if (car_spawn_down)
        {
            obstacles.Add(Instantiate(car, spawnPoint, Quaternion.Euler(90, 180, 0)));
        }

        else if (car_spawn_left)
        {
            obstacles.Add(Instantiate(car, spawnPoint, Quaternion.Euler(90, 270, 0)));
        }

        else if (car_spawn_right)
        {
            obstacles.Add(Instantiate(car, spawnPoint, Quaternion.Euler(90, 90, 0)));
        }

        else if (car_spawn_up)
        {
            obstacles.Add(Instantiate(car, spawnPoint, Quaternion.Euler(90, 0, 0)));
        }
    }
}
