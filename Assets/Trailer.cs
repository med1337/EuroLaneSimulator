using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Trailer : Vehicle
{
    public DamageSystem damage_system;

    public int cargo_value = (int)TransactionTypes.DELIVERY;
    public Text cargo_value_text;
    public GameObject LegsGameObject;
    public Truck MyTruck;
    [SerializeField] List<TrailRenderer> trails = new List<TrailRenderer>();
    private Quaternion start_rotation;
    private float _timer;
    private float _jointBreakDelay = 3.0f;
    private HingeJoint joint;
    private bool already_dead = false;


    public void CollisionEvent(Collision _other)
    {
        if (MyTruck != null)
        {
            MyTruck.CollisionEvent(_other);
        }

        if (_other.gameObject.tag != "Floor")
        {
            DamageCargo();
        }

        damage_system.EnvironmentCollision();
    }


    public void DamageCargo()
    {
        if (cargo_value > 0)
            cargo_value -= 50;
    }


    public void TriggerEvent(Collider _other)
    {
        if (_other.tag == "Intersection")
        {
        }
        else if (_other.tag == "Hazard")
        {
            Debug.Log("Trailer TriggerEvent");

            if (damage_system.invulnerable)
                return;

            if (MyTruck != null)
            {
                MyTruck.TriggerEvent(_other);
            }

            DamageCargo();
            damage_system.HazardCollision();
        }
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
        if (Dead && !already_dead)
        {
            already_dead = true;
            Invoke("TriggerNewObjective", 6);
        }

        if (!Dead)
            already_dead = false;

        if (cargo_value_text != null)
            UpdateCargoValueText();

        base.Update();
    }


    private void TriggerNewObjective()
    {
        GameManager.scene.money_panel.LogTransaction((int)TransactionTypes.FAILED_DELIVERY, "Failed Delivery");
        GameManager.scene.objective_manager.SetNewObjective();
        GameManager.scene.chat_display.DisplayJobFailedMessage();
    }


    private void UpdateCargoValueText()
    {
        var f = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberDecimalDigits = 0
        };
        var s = cargo_value.ToString("n", f);
        cargo_value_text.text = "$ " + s;
    }


    public override void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer >= _jointBreakDelay && joint != null)
        {
            if (joint.breakForce == 2000000)
                joint.breakForce = 20000;
        }

        base.FixedUpdate();
    }


    void OnJointBreak(float force)
    {
        LegsGameObject.SetActive(true);
        GameManager.scene.objective_manager.DetachedTrailer(false);
        Debug.Log(force.ToString());
        MyTruck.AttachedTrailer = null;
        MyTruck = null;
    }


    public Trailer AttachTrailer(Rigidbody truckRigidbody)
    {
        joint = gameObject.AddComponent(typeof(HingeJoint)) as HingeJoint;
        if (joint == null) return null;
        joint.connectedBody = truckRigidbody;
        joint.enableCollision = true;
        joint.breakForce = 2000000;
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
        foreach (var child in GetComponentsInChildren<Axis>())
        {
            child.LeftCollider.motorTorque = 0.01f;
            child.RightCollider.motorTorque = 0.01f;
        }

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0, 0, -0.5733331f);

        _timer = 0;


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
        Dead = false;
        base.MyRigidbody.constraints = RigidbodyConstraints.None;
        transform.position = _transform.position + new Vector3(0, 2);
        transform.rotation = start_rotation;
        MyRigidbody.maxAngularVelocity = 7;
        cargo_value = 1;
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