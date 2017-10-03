using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CustomMath
{
    public static float Map(float _value, float _i_start, float _i_stop, float _o_start, float _o_stop)
    {
        return _o_start + (_o_stop - _o_start) * ((_value - _i_start) / (_i_stop - _i_start));
    }
}
