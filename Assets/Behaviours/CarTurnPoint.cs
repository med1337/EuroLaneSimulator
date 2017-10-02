using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarRotation
{
    UP,
    DOWN = 180,
    LEFT = 270,
    RIGHT = 90
}

public class CarTurnPoint : MonoBehaviour
{
    [SerializeField] CarRotation ingress_rotation;
    [SerializeField] float turn_chance;


    void OnTriggerEnter(Collider _other)
    {
        CarMovement car = _other.GetComponent<CarMovement>();

        if (car == null)
            return;

        if (_other.transform.rotation.eulerAngles.y != (int)ingress_rotation)
            return;

        if (car.alive && Random.Range(1, 100) <= turn_chance)
            _other.transform.Rotate(Vector3.up * -90);
    }

}
