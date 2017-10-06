using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {


    public GameObject V1;
    public GameObject V2;
    public GameObject H1;
    public GameObject H2;



    // Use this for initialization
    void Start () {

        InvokeRepeating("ChangeLights", 2, 10);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeLights()
    {
        if (V1.transform.localPosition.y == 3)
        {
            Vector3 newPos = V1.transform.localPosition;

            newPos.y = -5;

            V1.transform.localPosition = newPos;



            newPos = V2.transform.localPosition;

            newPos.y = -5;

            V2.transform.localPosition = newPos;



            newPos = H1.transform.localPosition;

            newPos.y = 3;

            H1.transform.localPosition = newPos;



            newPos = H2.transform.localPosition;

            newPos.y = 3;

            H2.transform.localPosition = newPos;
        }

        else
        {
            Vector3 newPos = V1.transform.localPosition;

            newPos.y = 3;

            V1.transform.localPosition = newPos;



            newPos = V2.transform.localPosition;

            newPos.y = 3;

            V2.transform.localPosition = newPos;



            newPos = H1.transform.localPosition;

            newPos.y = -5;

            H1.transform.localPosition = newPos;



            newPos = H2.transform.localPosition;

            newPos.y = -5;

            H2.transform.localPosition = newPos;
        }

    }
}
