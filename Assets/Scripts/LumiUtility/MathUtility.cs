using System;
using UnityEngine;
public class MathUtility
{
    public static float PI = (float)Math.PI;
    public static float TwoPI = (float)(Math.PI * 2);
    public static readonly float DEG_TO_RAD = PI / 180.0f;
    public static readonly float RAD_TO_DEG = 180.0f / PI;

    public static float RadToDeg(float rad)
    {
        return RAD_TO_DEG * rad;
    }

    public static float DegToRad(float deg)
    {
        return DEG_TO_RAD * deg;
    }


    public static float ParseAngleToRadians(float angle)
    {
        return MathUtility.ConstrainPI(MathUtility.DegToRad(angle));
    }

    // [0, 2PI)
    public static float Constrain2Pi(float angle)
    {
        if (angle < 0)
        {
            int count = (int)(-angle / TwoPI) + 1;
            angle += count * TwoPI;
        }

        if (angle >= TwoPI)
        {
            int count = (int)(angle / TwoPI);
            angle -= count * TwoPI;
        }

        return angle;
    }

    // [-PI, PI)
    public static float ConstrainPI(float angle)
    {
        if (angle < -PI)
        {
            angle += PI * 2;
        }

        if (angle >= PI)
        {
            angle -= PI * 2;
        }

        return angle;
    }

    private static System.Random random = new System.Random();

    public static bool RandomBool(float rate)
    {
        return random.NextDouble() < rate;
    }

    public static bool RandomBool()
    {
        return RandomBool(0.5f);
    }

    public static float Random(float max)
    {
        return (float)random.NextDouble() * max;
    }

    public static float Random(float min, float max)
    {
        return min + (float)random.NextDouble() * (max - min);
    }

    public static int Random(int min, int max)
    {
        return (int)(min + (float)random.NextDouble() * (max - min));
    }

    public static int Random(int max)
    {
        return (int)(random.NextDouble() * max);
    }

    public static float GetAngleVector2(float disY, float disX)
    {
        if (disX == 0)
        {
            return disY > 0 ? 90 : -90;
        }
        else
        {
            float degree = RadToDeg((float)Math.Atan(Math.Abs(disY / disX)));

            if (disY >= 0)
            {
                if (disX <= 0)
                {
                    degree = 180 - degree;
                }
            }
            else
            {
                if (disX >= 0)
                {
                    degree = 360 - degree;
                }
                else
                {
                    degree = 180 + degree;
                }
            }
            return degree;
        }
    }

    // [-PI, PI)
    public static float AngleBetween(float x1, float y1, float x2, float y2)
    {
        return Math.Abs(ConstrainPI(AngleFromTo(x1, y1, x2, y2)));
    }

    // [0, 2PI)
    public static float AngleFromTo(float x1, float y1, float x2, float y2)
    {
        return Constrain2Pi((float)(Math.Atan2(y2, x2) - Math.Atan2(y1, x1)));
    }

    public static float AngleFromTo(Vector2 v, Vector2 v2)
    {
        return AngleFromTo(v.x, v.y, v2.x, v2.y);
    }

    public static float AngleFromToInAbsPi(Vector2 v, Vector2 v2)
    {
        float angleDelta = ConstrainPI(Mathf.Atan2(v2.y, v2.x) - Mathf.Atan2(v.y, v.x));
        return Math.Abs(angleDelta);
    }

    public static Vector2 Rotate(Vector2 v, float rotation)
    {
        float x2, y2;
        Rotate(v.x, v.y, rotation, out x2, out y2);
        return new Vector2(x2, y2);
    }

    public static void Rotate(float x, float y, float rotation, out float x2, out float y2)
    {
        float cos = Mathf.Cos(rotation);
        float sin = Mathf.Sin(rotation);
        float M11 = cos;
        float M12 = sin;
        float M21 = -sin;
        float M22 = cos;
        x2 = x * M11 + y * M21;
        y2 = x * M12 + y * M22;
    }

    public static Vector2 Bezier(Vector2 p00, Vector2 p10, Vector2 p20, float t)
    {
        var p11 = p00 * (1 - t) + p10 * t;
        var p21 = p10 * (1 - t) + p20 * t;
        var p22 = p11 * (1 - t) + p21 * t;

        return p22;
    }

    public static Vector3 Bezier(Vector3 p00, Vector3 p10, Vector3 p20, float t)
    {
        var p11 = p00 * (1 - t) + p10 * t;
        var p21 = p10 * (1 - t) + p20 * t;
        var p22 = p11 * (1 - t) + p21 * t;

        return p22;
    }

}
