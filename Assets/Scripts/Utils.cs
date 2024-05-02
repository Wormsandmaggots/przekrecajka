using UnityEngine;

public class Utils
{
    public static float Remap (float value, float from1, float to1, float from2, float to2) { 
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
        
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}