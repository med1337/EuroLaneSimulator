using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Trailer : Vehicle
{
    public DamageSystem damage_system;

    public int CargoValue = 100000;
    public Text CargoValText;
    public GameObject LegsGameObject;
    public Truck MyTruck;
    [SerializeField] List<TrailRenderer> trails = new List<TrailRenderer>();
    private Quaternion start_rotation;



    public void CollisionEvent(Collision _other)
    {
        if (MyTruck != null)
        {
            MyTruck.damage_system.EnvironmentCollision();
        }

        damage_system.EnvironmentCollision();
    }


    public void TriggerEvent(Collider _other)
    {
        if (_other.tag == "Intersection")
        {

        }
        else if (_other.tag == "Hazard")
        {
            if (damage_system.invulnerable)
                return;

            if (MyTruck != null)
            {
                MyTruck.damage_system.HazardCollision();
            }

            damage_system.HazardCollision();
        }
    }


    void OnMouseClick()
    {
        CargoValue -= 1337;
    }


    // Use this for initialization
    public override void Start()
    {
        start_rotation = transform.rotation;
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
        if (GameManager.scene.objective_manager != null)
            GameManager.scene.objective_manager.DetachedTrailer();

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
        joint.breakForce = 20000;
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


    public void ResetPosition(Transform _transform)
    {
        transform.position = _transform.position + new Vector3(0, 2);
        transform.rotation = start_rotation;
        ClearTrails();
    }


    private void ClearTrails()
    {
        if (trails == null)
            return;

        foreach (TrailRenderer trail in trails)
        {
            if (trail == null)
                continue;

            trail.Clear();
        }
    }
}