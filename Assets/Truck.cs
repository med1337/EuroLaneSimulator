using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    public WheelControlScript WheelControlScript;
    public Text SpeedText;
    public Rigidbody RigidBody;

    public float SpeedLimit;
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Speed = RigidBody.velocity.magnitude;

        if (SpeedText != null)
            SpeedText.text = RigidBody.velocity.magnitude.ToString();
	}

    void FixedUpdate()
    {
        if (RigidBody.velocity.magnitude > SpeedLimit)
        {
            RigidBody.velocity = RigidBody.velocity.normalized * SpeedLimit;
        }
    }
}
