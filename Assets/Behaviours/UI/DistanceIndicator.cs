using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DistanceIndicator : MonoBehaviour
{
    [SerializeField] private Slider distance_slider;
    [SerializeField] private Image trailer_image;
    [SerializeField] private Transform player_transform;
    [SerializeField] private ArrowLookAtTarget arrow_indicator;
    [SerializeField] private float lower_arrow_threshold = 0.05f;
    [SerializeField] private float upper_arrow_threshold = 0.95f;

    private Transform start_transform;
    private Transform end_transform;


    public void SetRoute(Transform _start_transform, Transform _end_transform)
    {
        if (distance_slider == null)
            return;

        end_transform = _start_transform;
        start_transform = _end_transform;

        if (arrow_indicator == null)
            return;

        arrow_indicator.SetTarget(end_transform);

        CalculateSliderMaxValue();
    }


    public void SetTrailerGraphic(bool _enabled)
    {
        trailer_image.enabled = _enabled;
    }


    private void CalculateSliderMaxValue()
    {
        if (NullChecks())
            return;

        float dist = (end_transform.position - start_transform.position).sqrMagnitude;
        distance_slider.maxValue = dist;
    }


    private void UpdateSliderValue()
    {
        if (NullChecks())
            return;

        float dist = (player_transform.position - end_transform.position).sqrMagnitude;
        distance_slider.value = dist;

        if (arrow_indicator == null)
            return;

        float percent = CustomMath.Map(distance_slider.value, 0, distance_slider.maxValue, 0, 1);
        if (percent < lower_arrow_threshold || percent > upper_arrow_threshold)
        {
            arrow_indicator.EnableArrow();
            return;
        }

        arrow_indicator.DisableArrow();
    }


    void Update ()
    {
        if (NullChecks())
            return;

        CalculateSliderMaxValue();
        UpdateSliderValue();
    }


    private bool NullChecks()
    {
        if (end_transform == null)
            return true;

        if (start_transform == null)
            return true;

        if (distance_slider.transform == null)
            return true;

        return false;
    }
}
