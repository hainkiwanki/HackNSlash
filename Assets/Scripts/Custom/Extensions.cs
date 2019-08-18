using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{ 
    public static Vector3 NewY(this Vector3 _v3, float _y)
    {
        return new Vector3(_v3.x, _y, _v3.z);
    }

    public static Vector2 ToVector2(this Vector3 _v3)
    {
        return new Vector2(_v3.x, _v3.y);
    }
}
