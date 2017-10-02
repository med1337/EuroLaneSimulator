using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    Truck player; //player GO
    private int minSpawnTime = 3; //minimum time to next spawn
    private int maxSpawnTime = 6; //maximum time to next spawn
    private int spawnDistance = 200; //distance from player to spawn a car
    public GameObject car; //car GO to spawn

    [SerializeField] CarRotation spawn_rotation;

    private bool thing_queued;

    // Use this for initialization
    void Start()
    {
        player = GameManager.scene.player_truck;
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

        Quaternion rot = new Quaternion();

        switch(spawn_rotation)
        {
            case CarRotation.UP: 
            rot = Quaternion.Euler(0, 0, 0);
            break;

            case CarRotation.DOWN: 
            rot = Quaternion.Euler(0, 180, 0);
            break;

            case CarRotation.LEFT: 
            rot = Quaternion.Euler(0, 270, 0);
            break;

            case CarRotation.RIGHT: 
            rot = Quaternion.Euler(0, 90, 0);
            break;

        }

        Instantiate(car, spawnPoint, rot);    
    }
}
