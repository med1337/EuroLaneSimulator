using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionAvoidance : MonoBehaviour {

    CarMovement parentScript;

    private AudioSource horn;

    // Use this for initialization
    void Start ()
    {
        parentScript = transform.parent.gameObject.GetComponent<CarMovement>();

        horn = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider c)
    {
        int speed = 0;

        if ((c.tag == "Hazard") || (c.tag == "Player") || c.tag == "Trailer")
        {
            float distance = Vector3.Distance(transform.position, c.transform.position);

            switch ((int)distance)
            {
                case 7:
                case 6:
                case 5:
                case 4:
                    speed = 3;
                    break;
                case 3:
                case 2:
                case 1:
                case 0:
                    speed = 0;
                    break;

                default:
                    speed = 6;
                    break;
            }


            parentScript.SetSpeed(speed);

        }
    }

    // Vehicle Horns
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player" || other.tag == "Hazard")
        {
            int honk_chance = Random.Range(0, 10);

            if(honk_chance < 1)
            {
                horn.Play();
            }

        }
    }

    void OnTriggerExit(Collider c)
    {
        //if ((c.tag == "Hazard") || (c.tag == "Player"))
        {
            parentScript.SetSpeed(6);
        }
    }
}
