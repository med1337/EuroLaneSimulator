using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Trailer : Vehicle
{
    public int CargoValue = 100000;
    public Text CargoValText;
    public GameObject LegsGameObject;
    public Truck MyTruck;
    void OnMouseClick()
    {
        CargoValue -= 1337;
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (CargoValText != null)
            UpdateCargoValueText();

        base.Update();
    }

    private void UpdateCargoValueText()
    {
        var f = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberDecimalDigits = 0
        };
        var s = CargoValue.ToString("n", f);
        CargoValText.text = "$" + s;
    }

    public override void FixedUpdate()
    {


        if (MyTruck != null)
        {
            base.FixedUpdate();

        }
    }

    void OnJointBreak(float force)
    {
        LegsGameObject.SetActive(true);
        Debug.Log(force.ToString());
        MyTruck.AttachedTrailer = null;
    }

    public Trailer AttachTrailer(Rigidbody truckRigidbody)
    {
        HingeJoint joint = gameObject.AddComponent(typeof(HingeJoint)) as HingeJoint;
        if (joint == null) return null;
        joint.connectedBody = truckRigidbody;
        joint.enableCollision = true;
        //joint.breakForce = 100000;
        joint.anchor = new Vector3(0, 0, 1);
        joint.axis = new Vector3(0, 1, 0);
        JointLimits limits = new JointLimits
        {
            min = 0,
            max = 0,
            bounciness = 0,
            bounceMinVelocity = 0,
            contactDistance = 0
        };
        joint.limits = limits;

        MyTruck = truckRigidbody.GetComponent<Truck>();

        foreach(var child in GetComponentsInChildren<Axis>())
        {
            child.LeftCollider.motorTorque = 0.01f;
            child.RightCollider.motorTorque = 0.01f;
        }

         joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0, 0, -0.5733331f);

        LegsGameObject.SetActive(false);
        return this;
    }

    public Trailer DetachTrailer()
    {
        var x = GetComponent<HingeJoint>();
        Destroy(x);
        LegsGameObject.SetActive(true);
        MyTruck = null;
        return null;
    }
}