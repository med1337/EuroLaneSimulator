using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionAvoidance : MonoBehaviour {

    CarMovement parentScript;

	// Use this for initialization
	void Start ()
    {
        parentScript = transform.parent.gameObject.GetComponent<CarMovement>();
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
                    speed = 4;
                    break;

                case 4:
                case 3:
                case 2:
                    speed = 2;
                    break;

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

    void OnTriggerExit(Collider c)
    {
        //if ((c.tag == "Hazard") || (c.tag == "Player"))
        {
            parentScript.SetSpeed(6);
        }
    }
}
