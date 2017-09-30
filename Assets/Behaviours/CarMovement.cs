using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public int speed = 3;
    private int despawnDistance = 100;
    private bool alive = true;
    GameObject player; //player GO

    // Use this for initialization
    void Start ()
    {
       //speed = Random.Range(1, 5);

       player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (alive)
        {
            transform.position += transform.up * Time.deltaTime * speed;
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

            int turn = Random.Range(0, 1);

            if (collision.gameObject.tag == "CarTurnLeft")
            {
                if (turn > 0)
                {
                    transform.Rotate(Vector3.forward * 90);
                }
            }

            if (collision.gameObject.tag == "CarTurnRight")
            {
                if (turn > 0)
                {
                    transform.Rotate(Vector3.forward * -90);
                }
            }
        }
    }
}
