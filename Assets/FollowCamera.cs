using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Truck truck;
    [SerializeField] float base_zoom = 29.09f;
    [SerializeField] float additional_zoom = 5;
    [SerializeField] float lead_factor;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 pos = new Vector3(0,20,0) + truck.transform.position;
        transform.position = lead_factor == 0 ? pos : pos + (truck.transform.forward * truck.Speed * lead_factor);

	    cam.orthographicSize = base_zoom + ((truck.Speed / truck.SpeedLimit) * additional_zoom);
	    cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, base_zoom, base_zoom + additional_zoom);
	}
}
