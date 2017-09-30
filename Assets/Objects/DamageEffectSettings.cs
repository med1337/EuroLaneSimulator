using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DamageEffect/Effect Settings")]
public class DamageEffectSettings : ScriptableObject
{
    public float shake_strength;
    public float shake_duration;

    public Color flash_color;
    public float flash_duration;
}
