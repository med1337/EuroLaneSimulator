using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DistanceIndicator : MonoBehaviour
{
    [SerializeField] private Slider distance_slider;
    [SerializeField] private Transform player_transform;
    [SerializeField] private Transform start_transform;
    [SerializeField] private Transform end_transform;


    public void SetRoute(Transform _start_transform, Transform _end_transform)
    {
        if (distance_slider == null)
            return;

        end_transform = _start_transform;
        start_transform = _end_transform;

        CalculateSliderMaxValue();
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

        float dist = (player_transform.position - end_transform.position  ).sqrMagnitude;
        distance_slider.value = dist;
    }


    // Update is called once per frame
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
