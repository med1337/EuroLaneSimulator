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

    [HideInInspector] public bool has_trailer = true;

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
        Speed /= 2;
        if (SpeedText != null)
        {
            var speedText = (int)Speed + " MPH";
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
}