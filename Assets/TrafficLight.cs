using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {


    public BoxCollider V1;
    public BoxCollider V2;
    public BoxCollider H1;
    public BoxCollider H2;



    // Use this for initialization
    void Start () {

        InvokeRepeating("ChangeLights", 2, 10);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeLights()
    {
        if (V1.GetComponent<BoxCollider>().isTrigger)
        {
            V1.GetComponent<BoxCollider>().isTrigger = false;
            V2.GetComponent<BoxCollider>().isTrigger = false;

            H1.GetComponent<BoxCollider>().isTrigger = true;
            H2.GetComponent<BoxCollider>().isTrigger = true;
        }

        else
        {
            V1.GetComponent<BoxCollider>().isTrigger = true;
            V2.GetComponent<BoxCollider>().isTrigger = true;

            H1.GetComponent<BoxCollider>().isTrigger = false;
            H2.GetComponent<BoxCollider>().isTrigger = false;
        }

    }
}
