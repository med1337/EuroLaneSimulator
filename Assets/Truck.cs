using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    public WheelControlScript WheelControlScript;
    public Text SpeedText;

    public float SpeedLimit;
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Speed = GetComponent<Rigidbody>().velocity.magnitude;
        SpeedText.text = GetComponent<Rigidbody>().velocity.magnitude.ToString();
	}

    void FixedUpdate()
    {
        if (GetComponent< Rigidbody >().velocity.magnitude > SpeedLimit)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * SpeedLimit;
        }
    }
}
