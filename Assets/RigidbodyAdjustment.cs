using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyAdjustment : MonoBehaviour
{

    public Vector3 RbadjustVector3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<Rigidbody>().centerOfMass = RbadjustVector3;
	}

    void OnDrawGizmosSelected()
    {
        Vector3 com = GetComponent<Rigidbody>().centerOfMass;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(com), 0.15f);
    }
}
