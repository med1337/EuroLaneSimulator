using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform GameObjecTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 x = new Vector3(0,1,0);
	    transform.position = GameObjecTransform.position + x;

	}
}
