using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] List<FadableGraphic> fades;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] List<ShakeModule> shakes;
    [SerializeField] float flash_duration;
    [SerializeField] float shake_strength;
    [SerializeField] float shake_duration;
    [SerializeField] float invulnerability_duration;
    [SerializeField] float invulnerability_opacity;
    [SerializeField] float invulnerability_variance;
    
    private bool invulnerable;


    public void Damage(int _damage)
    {
        if (invulnerable)
            return;

        invulnerable = true;

        StopAllCoroutines();
        StartCoroutine(DamageEffect());
    }


    void Update()
    {
        if (invulnerable)
            HandleInvulnerableFlicker();
    }


    IEnumerator DamageEffect()
    {
        foreach (FadableGraphic fade in fades)
        {
            fade.SetBaseColor(Color.white);
            fade.FadeOut(flash_duration);
        }

        foreach (ShakeModule shake in shakes)
            shake.Shake(shake_strength, shake_duration);

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

}
