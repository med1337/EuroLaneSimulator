using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControlScript : MonoBehaviour
{

    public float motor;
    public float steering;

    public float SteeringAngle;

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
        motor = MotorTorque * Input.GetAxis("Vertical");
        steering = SteeringAngle * Input.GetAxis("Horizontal");

        foreach (Axis axleInfo in AxlesList)
        {
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
        }
    }
}