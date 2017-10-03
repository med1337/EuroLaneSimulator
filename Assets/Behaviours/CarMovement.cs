using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public int speed = 6;
    private int despawnDistance = 400;
    public bool alive = true;
    Truck player; //player GO

    public SpriteRenderer carSprite;

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
            //if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "Hazard"))
            if (collision.gameObject.tag == "Player")
            {
                alive = false;      
            }
        }


    }   
    
    public void SetSpeed(int spd)
    {
        speed = spd;
    }

    void OnBecameInvisible()
    {
        if (!alive)
        {
            Destroy(this.gameObject);
        }
    }
}
