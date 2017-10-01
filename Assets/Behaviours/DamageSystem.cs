using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] List<FadableGraphic> fades;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] List<ShakeModule> shakes;

    [SerializeField] DamageEffectSettings environment_effect_settings;
    [SerializeField] DamageEffectSettings hazard_effect_settings;

    [SerializeField] float invulnerability_duration;
    [SerializeField] float invulnerability_opacity;
    [SerializeField] float invulnerability_variance;
    
    private bool invulnerable;


    public void EnvironmentCollision(Collision _other)
    {
        DamageEffect(environment_effect_settings);
    }


    public void HazardCollision(Collider _other)
    {
        if (invulnerable || _other.tag != "Hazard")
            return;

        DamageEffect(hazard_effect_settings);
        invulnerable = true;

        StopAllCoroutines();
        StartCoroutine(InvulnerabilityEnumerator());
    }


    void Update()
    {
        if (invulnerable)
            HandleInvulnerableFlicker();
    }


    void DamageEffect(DamageEffectSettings _settings)
    {
        foreach (FadableGraphic fade in fades)
        {
            fade.SetBaseColor(_settings.flash_color);
            fade.FadeOut(_settings.flash_duration);
        }

        foreach (ShakeModule shake in shakes)
            shake.Shake(_settings.shake_strength, _settings.shake_duration);
    }


    IEnumerator InvulnerabilityEnumerator()
    {
        yield return new WaitForSeconds(invulnerability_duration);

        invulnerable = false;

        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 1;

            sprite.color = color;
        }
    }


    void HandleInvulnerableFlicker()
    {
        float flicker_opacity = invulnerability_opacity + Random.Range(-invulnerability_variance,
            invulnerability_variance);

        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = flicker_opacity;

            sprite.color = color;
        }
    }


    void OnCollisionEnter(Collision _other)
    {
        Debug.Log(_other.gameObject.name);
    }

}
