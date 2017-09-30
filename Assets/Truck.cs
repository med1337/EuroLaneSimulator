using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : Vehicle
{
    public WheelControlScript WheelControlScript;
    public Text SpeedText;

    public float SpeedLimit;
    public float Speed;

    public bool TrailerInReach;

    // Update is called once per frame
    public override void Update()
    {
        CheckSpeed();
        base.Update();
    }

    private void CheckSpeed()
    {
        var localSpeed = transform.InverseTransformDirection(MyRigidbody.velocity);
        Speed = localSpeed.z;
        Speed *= 2.237f;
        if (SpeedText != null)
        {
            var speedText = (int) Speed + " MPH";
            SpeedText.text = speedText;
        }
    }

    public override void FixedUpdate()
    {
        if (MyRigidbody.velocity.magnitude > SpeedLimit)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * SpeedLimit;
        }
        base.FixedUpdate();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Trailer"))
        {
            TrailerInReach = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Trailer"))
        {
            TrailerInReach = false;
        }
    }
}