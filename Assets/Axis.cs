using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    public bool Steering;
    public bool Motor;
    public bool Handbrake;

    private float _motor;
    private float _braking;
    private float _steering;
    private float _handbrake;


    public WheelCollider LeftCollider;
    public WheelCollider RightCollider;

    public Axis(float motor, float braking, float steering)
    {
        _motor = motor;
        _braking = braking;
        _steering = steering;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateParameters(float motor, float steering, float braking, float handbrake)
    {
        _motor = motor;
        _steering = steering;
        _braking = braking;
        _handbrake = handbrake;
    }

    void FixedUpdate()
    {
        LeftCollider.brakeTorque = _braking;
        RightCollider.brakeTorque = _braking;
        if (Steering)
        {
            LeftCollider.steerAngle = _steering;
            RightCollider.steerAngle = _steering;
        }
        if (Motor)
        {
            LeftCollider.motorTorque = _motor;
            RightCollider.motorTorque = _motor;
        }
        if (Handbrake)
        {
            LeftCollider.brakeTorque = _handbrake;
            RightCollider.brakeTorque = _handbrake;
        }
    }
}