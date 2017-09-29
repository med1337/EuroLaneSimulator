using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControlScript : MonoBehaviour
{
    public float motor;
    public float steering;
    public float braking;
    public float reverse; 

    public float SteeringAngle;
    public float BrakeForce;
    public float Speed;

    public float MotorTorque;
    public List<Axis> AxlesList;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        motor = MotorTorque * Input.GetAxis("Forward");
        steering = SteeringAngle * Input.GetAxis("Horizontal");
        braking = BrakeForce * Input.GetAxis("Brake");
        reverse = MotorTorque * Input.GetAxis("Reverse");
        if (reverse > 0)
        {
            motor = -reverse;
        }

        foreach (Axis axleInfo in AxlesList)
        {
            if (motor != 0)
            {
                axleInfo.LeftCollider.motorTorque = 0.01f;
                axleInfo.RightCollider.motorTorque = 0.01f;
            }
            if (axleInfo.Steering)
            {
                axleInfo.LeftCollider.steerAngle = steering;
                axleInfo.RightCollider.steerAngle = steering;
            }
            if (axleInfo.Motor)
            {
                axleInfo.LeftCollider.motorTorque = motor;
                axleInfo.RightCollider.motorTorque = motor;
            }
            axleInfo.LeftCollider.brakeTorque = braking;
            axleInfo.RightCollider.brakeTorque = braking;
        }
    }
}