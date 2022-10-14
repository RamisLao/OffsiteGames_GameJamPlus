using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CMath
{
    public static float Normalize(float i, float min, float max)
    {
        return (i - min) / (max - min);
    }

    public static float Denormalize(float j, float min, float max)
    {
        return j * (max - min) + min;
    }
}
