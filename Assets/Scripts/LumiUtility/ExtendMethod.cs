using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendMethod
{
    public static Vector3 ToVector3(this float[] self)
    {
        Vector3 vec = Vector3.zero;
        for (int i = 0; i < self.Length; i++) 
        {
            vec[i] = self[i];
        }
        return vec;
    }
    public static Vector3 ToVector3(this int[] self)
    {
        Vector3 vec = Vector3.zero;
        for (int i = 0; i < self.Length; i++)
        {
            vec[i] = self[i];
        }
        return vec;
    }
}
