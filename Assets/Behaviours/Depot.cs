using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Depot : MonoBehaviour
{
    [HideInInspector] public int job_value = 0;
    [HideInInspector] public int penalty_value = 0;
    [HideInInspector] public bool trailer_delivered = false;
    [HideInInspector] public BoxCollider delivery_area = null;


    void Start()
    {
        delivery_area = GetComponent<BoxCollider>();
    }


    private void OnCollisionStay(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Truck"))
            trailer_delivered = true;

        //Possible stretch goal: implement delivery accuracy
    }


}
