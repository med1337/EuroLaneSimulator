using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag != "Player")
            return;

        var damage_system = _other.gameObject.GetComponent<DamageSystem>();

        if (damage_system == null)
            damage_system = _other.transform.parent.GetComponent<DamageSystem>();

        if (damage_system == null)
            return;

        damage_system.Damage(0);
    }
}
