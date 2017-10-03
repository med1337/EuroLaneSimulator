using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : Vehicle
{
    public DamageSystem damage_system;

    public WheelControlScript WheelControlScript;
    public Text SpeedText;
    public Trailer PossibleTrailer;
    public Trailer AttachedTrailer;
    public float SpeedLimit;
    public float Speed;
    private float AttachDelay = 0.5f;
    private float _timer = 0.0f;
    [HideInInspector] public bool has_trailer = true;
    public bool QueueAttachment { get; set; }

    [Space] [Header("Engine Audio")] [SerializeField] private AudioSource engine_audio_source;
    [SerializeField] private float pitch_to_speed_reduction = 8;
    [SerializeField] private float minimum_pitch = 0.9f;
    [SerializeField] private float maximum_pitch = 1.9f;


    public void CollisionEvent(Collision _other)
    {
        if (AttachedTrailer != null)
        {
            AttachedTrailer.damage_system.EnvironmentCollision();
        }

        damage_system.EnvironmentCollision();

        if (_other.gameObject.tag == "Hazard")
        {
            GameManager.scene.money_panel.LogTransaction((int) TransactionTypes.COLLISION, "Vehicle Collision");
        }
    }


    public void TriggerEvent(Collider _other)
    {
        if (_other.tag == "Intersection")
        {
            if (Speed >= GameManager.ROAD_SPEED_LIMIT)
            {
                GameManager.scene.money_panel.LogTransaction((int) TransactionTypes.SPEEDING, "Speeding Violation");
            }
        }
        else if (_other.tag == "Hazard")
        {
            if (damage_system.invulnerable)
                return;

            if (AttachedTrailer != null)
            {
                AttachedTrailer.damage_system.HazardCollision();
            }

            damage_system.HazardCollision();

            GameManager.scene.money_panel.LogTransaction((int) TransactionTypes.COLLISION, "Vehicle Collision");
        }
    }


    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonUp("Attach"))
        {
            if (AttachedTrailer)
            {
                ProcessTrailer();
            }
            else
            {
                QueueAttachment = true;
                _timer = 0;
            }
        }
        if (QueueAttachment)
        {
            _timer += Time.deltaTime;
            if (Math.Abs(TipAngle) < 0.001f && Math.Abs(Speed) < 1)
            {
                _timer = 0;
                ProcessTrailer();
                QueueAttachment = false;
            }
            if (_timer >= AttachDelay)
            {
                _timer = 0;
                QueueAttachment = false;
            }
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

        if (Mathf.Abs(Speed) < 0.1f)
        {
            Speed = 0;
        }
        if (SpeedText != null)
        {
            var speedText = (int) Speed + " MPH";
            SpeedText.text = speedText;
        }

        if (engine_audio_source == null)
            return;


        engine_audio_source.pitch = CustomMath.Map(Speed, 0, SpeedLimit, minimum_pitch, maximum_pitch);
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
                MyRigidbody.constraints = RigidbodyConstraints.None;
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
    }

    public void ProcessTrailer()
    {
        _timer = 0.0f;
        //MyRigidbody.constraints=RigidbodyConstraints.FreezeAll;
        if (AttachedTrailer != null) //detaching trailer
        {
            AttachedTrailer = PossibleTrailer.DetachTrailer();

            if (GameManager.scene.objective_manager != null)
                GameManager.scene.objective_manager.DetachedTrailer();

            if (GameManager.scene.distance_indicator != null)
                GameManager.scene.distance_indicator.SetTrailerGraphic(false); //just in case
            return;
        }

        if (PossibleTrailer == AttachedTrailer) //else check for attach
            return;

        AttachedTrailer = PossibleTrailer.AttachTrailer(MyRigidbody);

        AudioManager.PlayOneShot("Trailer_Connected");

        if (GameManager.scene.objective_manager != null)
            GameManager.scene.objective_manager.AttachedTrailer();

        if (GameManager.scene.distance_indicator != null)
            GameManager.scene.distance_indicator.SetTrailerGraphic(true); //just in case
    }


    void OnTriggerExit(Collider other)
    {
        if (other.name == "AttachTrigger")
        {
            PossibleTrailer = null;
        }
    }
}