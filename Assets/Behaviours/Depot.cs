using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Depot : MonoBehaviour
{
    [HideInInspector] public int job_value = 0;
    [HideInInspector] public int penalty_value = 0;
    [HideInInspector] public bool trailer_delivered = false;
    public BoxCollider delivery_area = null;


    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Trailer"))
            trailer_delivered = true;

        //Possible stretch goal: implement delivery accuracy
    }


}
