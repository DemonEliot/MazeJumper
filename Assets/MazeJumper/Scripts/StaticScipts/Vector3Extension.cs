using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static Vector2 AsVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
