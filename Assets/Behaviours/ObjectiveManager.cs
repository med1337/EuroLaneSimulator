using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private static GameObject[] depots;


	// Use this for initialization
	void Start ()
	{
	    depots = GameObject.FindGameObjectsWithTag("Depot");
	}
	
}
