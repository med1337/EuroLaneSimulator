using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControlScript : MonoBehaviour
{
    public float motor;
    private float steering;
    private float braking;
    private float reverse;

    public float SteeringAngle;
    public float BrakeForce;
    public float Speed;

    public float MotorTorque;
    public List<Axis> AxlesList;
    public bool FreezeControls;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!FreezeControls)
        {
            motor = MotorTorque * Input.GetAxis("Forward");
            steering = SteeringAngle * Input.GetAxis("Horizontal");
            braking = BrakeForce * Input.GetAxis("Brake");
            reverse = MotorTorque * Input.GetAxis("Reverse");
        }
    }

    void FixedUpdate()
    {
        if (reverse > 0)
        {
            motor = -reverse;
        }

        foreach (Axis axleInfo in AxlesList)
        {
            if (axleInfo == null)
                continue;

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

    public void AttachBrake()
    {
        braking = 1000;
    }
}