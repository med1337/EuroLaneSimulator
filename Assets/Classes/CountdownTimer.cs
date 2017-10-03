using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CountdownTimer
{
    public float timer_duration { get; set; }
    public float current_time { get; set; }


    public void InitCountDownTimer(float _seconds)
    {
        timer_duration = _seconds;
        current_time = timer_duration;
    }


    public bool UpdateTimer()
    {
        current_time -= Time.deltaTime;

        if (current_time > 0)
            return false;

        current_time = timer_duration;
        return true;
    }

}
