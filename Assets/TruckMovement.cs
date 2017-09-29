using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private Rigidbody _myRigidbody;
    public float SpeedLimit;
    public float Acceleration;
    public float MaxTurningForce;
    public float CurrentSpeed;
    public float BrakingForce;

    public float TurnForce;

    // Use this for initialization
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed = _myRigidbody.velocity.magnitude;
    }

    float Remap(float currentValue, float minimumOne, float maximumOne, float minimumTwo, float maximumTwo)
    {
        return ((currentValue - minimumOne) / (maximumOne - minimumOne) * (maximumTwo - minimumTwo)) + minimumTwo;
    }

    void FixedUpdate()
    {
        {
            _myRigidbody.AddForce(transform.up * Input.GetAxis("Vertical") * Acceleration);
            //if (_myRigidbody2D.velocity.magnitude > 0)
            //{
            //    if (Input.GetKey(KeyCode.LeftArrow))
            //    {
            //        _myRigidbody2D.AddTorque(turningForce);
            //    }
            //    if (Input.GetKey(KeyCode.RightArrow))
            //    {
            //        _myRigidbody2D.AddTorque(-turningForce);
            //    }
            //}
        }
    }
}