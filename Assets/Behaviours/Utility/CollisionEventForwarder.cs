using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventForwarder : MonoBehaviour
{
    [SerializeField] CustomEvents.CollisionEvent collision_event;
    [SerializeField] CustomEvents.ColliderEvent collider_event;
    [SerializeField] CustomEvents.Collision2DEvent collision_2d_event;
    [SerializeField] CustomEvents.Collider2DEvent collider_2d_event;


    void OnCollisionEnter(Collision _other)
    {
        if (collision_event != null)
            collision_event.Invoke(_other);
    }


    void OnTriggerEnter(Collider _other)
    {
        if (collider_event != null)
            collider_event.Invoke(_other);
    }


    void OnCollisionEnter2D(Collision2D _other)
    {
        if (collision_2d_event != null)
            collision_2d_event.Invoke(_other);
    }


    void OnTriggerEnter2D(Collider2D _other)
    {
        if (collider_2d_event != null)
            collider_2d_event.Invoke(_other);
    }

}
