using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YMath
{
    public static float Remap(float value, float minOld, float maxOld, float minNew, float maxNew)
    {
        return (value - minOld) / (maxOld - minOld) * (maxNew - minNew) + minNew;
    }

    public static float RemapClamped(float value, float minOld, float maxOld, float minNew, float maxNew)
    {
        return Mathf.Clamp(Remap(value, minOld, maxOld, minNew, maxNew), minNew, maxNew);
    }
}
