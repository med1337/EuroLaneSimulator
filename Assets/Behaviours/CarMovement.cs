using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public int speed = 3;
    private int despawnDistance = 100;
    public bool alive = true;
    Truck player; //player GO

    // Use this for initialization
    void Start ()
    {
       //speed = Random.Range(1, 5);

       player = GameManager.scene.player_truck;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (alive)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > despawnDistance)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
            if (collision.gameObject.tag == "Player")
            {
                alive = false;                
            }
        }
    }
}
