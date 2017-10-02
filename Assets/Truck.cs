using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : Vehicle
{
    public WheelControlScript WheelControlScript;
    public Text SpeedText;
    public Trailer PossibleTrailer;
    public Trailer AttachedTrailer;
    public float SpeedLimit;
    public float Speed;
    private float AttachDelay = 0.5f;
    private float _timer =0.0f;
    [HideInInspector] public bool has_trailer = true;


    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonUp("Attach"))
        {
            ProcessTrailer();
        }
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
        if (_timer < AttachDelay)
        {
            _timer += Time.fixedDeltaTime;
        }
        else
        {
            if (MyRigidbody.constraints == RigidbodyConstraints.FreezeAll)
            {
                MyRigidbody.constraints=RigidbodyConstraints.None;
            }
        }

        base.FixedUpdate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "AttachTrigger")
        {
            PossibleTrailer = other.GetComponentInParent<Trailer>();
        }
        Debug.Log(other.name);
    }

    public void ProcessTrailer()
    {
        _timer = 0.0f;
        //MyRigidbody.constraints=RigidbodyConstraints.FreezeAll;
        if (AttachedTrailer != null)
        {
            AttachedTrailer = PossibleTrailer.DetachTrailer();
        }
        else if( PossibleTrailer!=AttachedTrailer)
        {
            AttachedTrailer = PossibleTrailer.AttachTrailer(MyRigidbody);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "AttachTrigger")
        {
            PossibleTrailer = null;
        }
    }
}