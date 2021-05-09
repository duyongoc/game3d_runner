using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    
    public static IEnumerator DelayEvent(Action callback, float timer)
    {
        yield return new WaitForSeconds(timer);
        callback();
    }

    public static Vector2 Angle2Rad(int angle)
    {
        float rad = Mathf.Deg2Rad * angle;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static float Rad2Angle(Vector2 rad)
    {
        float angle = (Mathf.Rad2Deg * Mathf.Acos(rad.x)) * (rad.y / Mathf.Abs(rad.y));
        return angle;
    }

    public static int AngleNormalize(int angle)
    {
        angle %= 360;
        if (angle < 0)
            angle += 360;
        return angle;
    }
}
