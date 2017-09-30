using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class Vehicle : MonoBehaviour
{
    public float TipAngle;
    protected Rigidbody MyRigidbody;

    //change this value to adjust ease of tipping
    private readonly float _tipLiftForce = 7.5f;

    private bool _flipped;
    private bool _rotated;

    [Header("Sprites, do not use more than 4 atm")] public SpriteRenderer CurrentSprite;
    public List<Sprite> TippingSpritesList;
    private float[] AngleSpriteLevels = {2.5f, 5, 89};


    public virtual void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        if (TipAngle > 45)
        {
            _flipped = true;
            if (CurrentSprite.flipX)
            {
                CurrentSprite.flipX = false;
            }
        }
        else
        {
            _flipped = false;
        }
        if (TipAngle < AngleSpriteLevels[0])
        {
            CurrentSprite.sprite = TippingSpritesList[0];
        }

        else if (TipAngle >= AngleSpriteLevels[0] && TipAngle < AngleSpriteLevels[1])
        {
            CurrentSprite.sprite = TippingSpritesList[1];
        }
        else if (TipAngle >= AngleSpriteLevels[1] && TipAngle < AngleSpriteLevels[2])
        {
            CurrentSprite.sprite = TippingSpritesList[2];
        }
        else if (TipAngle >= 89)
        {
            CurrentSprite.sprite = TippingSpritesList[3];
        }
        if (_flipped && !_rotated)
        {
            CurrentSprite.transform.Rotate(0, 90, 0);
            _rotated = true;
        }
        else if (!_flipped && _rotated)
        {
            CurrentSprite.transform.Rotate(0, -90, 0);
            _rotated = false;
        }
    }

    public virtual void FixedUpdate()
    {
        TipAngle = Vector3.Angle(transform.up, Vector3.up);
        var dirAngle = Vector3.Angle(transform.right, Vector3.up);
        if (dirAngle >= 89)
        {
            CurrentSprite.flipX = false;
        }
        else if (dirAngle <= 89)
        {
            CurrentSprite.flipX = true;
        }
        if (TipAngle > 0.5 && TipAngle < 30)
        {
            MyRigidbody.AddForceAtPosition(Vector3.up * TipAngle * _tipLiftForce, Vector3.zero);
        }
        else if (TipAngle >= 89)
        {
            MyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}