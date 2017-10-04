using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Manufacturer
{
    DAF = 0,
    IVECO,
    MAN,
    MERCEDES,
    RENAULT,
    SCANIA,
    VOLVO,
    LEO
}

public enum SteeringInfo
{
    AWD = 0,
    FWD = 1,
    RWD = 2
}

public struct TruckParameters
{
    public float _steeringAngle;
    public float _brakeForce;
    public float _motorTorque;
}

public class Truck : Vehicle
{
    struct InputControls
    {
        public float motor;
        public float steering;
        public float braking;
        public bool attach;
        public float handbrake;
        public bool frozen;
    }

    public Manufacturer TruckManufacturer;
    public TruckParameters MyTruckParameters;
    private InputControls myInputControls;

    public DamageSystem damage_system;

    public WheelControlScript WheelControlScript;
    public Text SpeedText;
    public Trailer PossibleTrailer;
    public Trailer AttachedTrailer;
    public float SpeedLimit;
    public float Speed;
    private float AttachDelay = 0.5f;
    private float _timer = 0.0f;
    private float _freezeTimer;
    private float _attachmentDuration = 1.0f;
    private float _failTimer = 0.0f;

    [HideInInspector] public bool has_trailer = true;
    public bool QueueAttachment { get; set; }

    [Space] [Header("Engine Audio")] [SerializeField] private AudioSource engine_audio_source;
    [SerializeField] private float pitch_to_speed_reduction = 8;
    [SerializeField] private float minimum_pitch = 0.9f;
    [SerializeField] private float maximum_pitch = 1.9f;

    [Header("Drive settings")] [SerializeField] private float _steeringAngle;
    [SerializeField] private float _brakeForce;
    [SerializeField] private float _motorTorque;


    public void CollisionEvent(Collision _other)
    {
        if (AttachedTrailer != null)
        {
            AttachedTrailer.damage_system.EnvironmentCollision();
            GameManager.scene.objective_manager.ReduceJobValue();
        }

        damage_system.EnvironmentCollision();

        if (_other.gameObject.tag == "Hazard")
        {
            GameManager.scene.money_panel.LogTransaction((int)TransactionTypes.COLLISION, "Vehicle Collision");
        }
    }

    public void TriggerEvent(Collider _other)
    {
        if (_other.tag == "Intersection")
        {
            if (Speed >= GameManager.ROAD_SPEED_LIMIT)
            {
                GameManager.scene.money_panel.LogTransaction((int)TransactionTypes.SPEEDING, "Speeding Violation");
            }
        }
        else if (_other.tag == "Hazard")
        {
            if (damage_system.invulnerable)
                return;

            if (AttachedTrailer != null)
            {
                AttachedTrailer.damage_system.HazardCollision();
                GameManager.scene.objective_manager.ReduceJobValue();
            }

            damage_system.HazardCollision();

            GameManager.scene.money_panel.LogTransaction((int)TransactionTypes.COLLISION, "Vehicle Collision");
        }
    }

    public override void Start()
    {
        base.Start();
        switch (TruckManufacturer)
        {
            case Manufacturer.DAF:
                MyTruckParameters._motorTorque = 360;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1000;
                break;
            case Manufacturer.IVECO:
                MyTruckParameters._motorTorque = 400;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1100;
                break;
            case Manufacturer.MAN:
                MyTruckParameters._motorTorque = 450;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1200;
                break;
            case Manufacturer.MERCEDES:

                MyTruckParameters._motorTorque = 480;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1300;
                break;
            case Manufacturer.RENAULT:
                MyTruckParameters._motorTorque = 500;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1400;
                break;
            case Manufacturer.SCANIA:
                MyTruckParameters._motorTorque = 550;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1500;
                break;
            case Manufacturer.VOLVO:
                MyTruckParameters._motorTorque = 600;
                MyTruckParameters._brakeForce = 300;
                MyTruckParameters._steeringAngle = 60;
                MyRigidbody.mass = 1600;
                break;

            case Manufacturer.LEO:
                var ax = SteeringInfo.RWD;
                MyTruckParameters._motorTorque = 500;
                MyTruckParameters._brakeForce = 400;
                MyTruckParameters._steeringAngle = 75;
                MyRigidbody.mass = 750;
                //for (var index = 0; index < AxlesList.Length; index++)
                //{
                //    AxlesList[index].Motor = false;
                //    if (index > 0 && ax == SteeringInfo.RWD)
                //    {
                //        AxlesList[index].Motor = true;
                //    }
                //    var axise = AxlesList[index];
                for (var index = 1; index < AxlesList.Length; index++)
                {
                    var axise = AxlesList[index];
                    WheelFrictionCurve x = axise.LeftCollider.sidewaysFriction;
                    x.stiffness = 1;
                    axise.LeftCollider.sidewaysFriction = x;
                    axise.RightCollider.sidewaysFriction = x;
                }
                //}
                break;
        }
    }

    private void GetButtons()
    {
        myInputControls.attach = Input.GetButtonUp("Attach");
        if (Input.GetButton("Handbrake"))
        {
            myInputControls.handbrake = MyTruckParameters._brakeForce;
        }
        else
        {
            myInputControls.handbrake = 0;
        }
    }

    private void UpdateControls()
    {
        GetButtons();

        if (myInputControls.frozen)
        {
            _freezeTimer += Time.deltaTime;

            if (!(_freezeTimer >= _attachmentDuration)) return;

            myInputControls.frozen = false;
            _freezeTimer = 0;
            return;
        }


        myInputControls.braking = MyTruckParameters._brakeForce * Input.GetAxis("Brake");
        var reverse = MyTruckParameters._motorTorque * Input.GetAxis("Reverse");
        myInputControls.motor = MyTruckParameters._motorTorque * Input.GetAxis("Forward");
        if (reverse > 0 && Speed > 0)
        {
            myInputControls.braking = MyTruckParameters._brakeForce;
        }
        else if (myInputControls.motor > 0 && Speed < 0)
        {
            myInputControls.braking = MyTruckParameters._brakeForce;
        }
        else if (reverse > 0)
        {
            myInputControls.motor = -reverse;
        }

        myInputControls.steering = MyTruckParameters._steeringAngle * Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Dead)
            Invoke("TriggerGameOver", 5);


        //get controls
        UpdateControls();

        //process attach button delay
        if (_timer < AttachDelay)
        {
            _timer += Time.deltaTime;
        }
        if ((myInputControls.attach && _timer >= AttachDelay) || QueueAttachment)
        {
            _timer = 0.0f;
            ProcessTrailer();
        }

        CheckSpeed();
        base.Update();
    }


    private void TriggerGameOver()
    {
        GameManager.GameOver();
    }


    private void CheckSpeed()
    {
        var localSpeed = transform.InverseTransformDirection(MyRigidbody.velocity);
        Speed = localSpeed.z;
        Speed *= 2.237f;
        Speed /= 2;

        if (Mathf.Abs(Speed) < 0.1f)
        {
            Speed = 0;
        }
        if (SpeedText != null)
        {
            var speedText = (int)Speed + " MPH";
            SpeedText.text = speedText;
        }

        if (engine_audio_source == null)
            return;


        engine_audio_source.pitch = CustomMath.Map(Mathf.Abs(Speed), 0, SpeedLimit, minimum_pitch, maximum_pitch);
    }

    public override void FixedUpdate()
    {
        if (MyRigidbody.velocity.magnitude > SpeedLimit)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * SpeedLimit;
        }
        foreach (var axise in AxlesList)
        {
            axise.UpdateParameters(myInputControls.motor, myInputControls.steering, myInputControls.braking,
                myInputControls.handbrake);
        }
        base.FixedUpdate();
    }

    public void AttachTrailer()
    {
        //put a brake
        myInputControls.frozen = true;
        myInputControls.braking = 1000;
        PossibleTrailer.transform.rotation.Set(transform.rotation.x, PossibleTrailer.transform.rotation.y,
            transform.rotation.z, PossibleTrailer.transform.rotation.w);

        if (Math.Abs(TipAngle) < 0.1f && Math.Abs(Speed) < 1.0f && Math.Abs(PossibleTrailer.TipAngle) < 5)
        {
            AttachedTrailer = PossibleTrailer.AttachTrailer(MyRigidbody);
            QueueAttachment = false;
            _failTimer = 0;
            AudioManager.PlayOneShot("Trailer_Connected");

            if (GameManager.scene.objective_manager != null)
                GameManager.scene.objective_manager.AttachedTrailer();

            if (GameManager.scene.distance_indicator != null)
                GameManager.scene.distance_indicator.SetTrailerGraphic(true); //just in case
        }
        else
        {
            _failTimer += Time.deltaTime;
            if (_failTimer > 1)
            {
                QueueAttachment = false;
            }
            else
            {
                QueueAttachment = true;
            }
        }
    }

    public void DetachTrailer()
    {
        AttachedTrailer = PossibleTrailer.DetachTrailer();

        AudioManager.PlayOneShot("Trailer_Disonnected");

        if (GameManager.scene.objective_manager != null)
            GameManager.scene.objective_manager.DetachedTrailer(true);

        if (GameManager.scene.distance_indicator != null)
            GameManager.scene.distance_indicator.SetTrailerGraphic(false); //just in case
    }

    public void ProcessTrailer()
    {
        if (AttachedTrailer)
        {
            DetachTrailer();
        }
        else if (PossibleTrailer)
        {
            AttachTrailer();
        }


        //else
        //{
        //    if (MyRigidbody.constraints == RigidbodyConstraints.FreezeAll)
        //    {
        //        MyRigidbody.constraints = RigidbodyConstraints.None;
        //    }
        //}
    }


    void OnTriggerExit(Collider other)
    {
        if (other.name == "AttachTrigger")
        {
            PossibleTrailer = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "AttachTrigger")
        {
            PossibleTrailer = other.GetComponentInParent<Trailer>();
        }
    }
}