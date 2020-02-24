using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
    static Vector3 TmpV3A = Vector3.zero;

    #region Reset
    public static void ResetPosition(this Transform transform, bool worldSpace=false)
    {
        if(worldSpace)
        {
            transform.position = Vector3.zero;
        }else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public static void ResetRotation(this Transform transform,bool worldSpace=false)
    {
        if(worldSpace)
        {
            transform.rotation = Quaternion.identity;
        }else
        {
            transform.localRotation = Quaternion.identity;
        }
    }

    public static void  ResetLocalScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }
    #endregion

    #region SetPositon
    public static void SetPosition(this Transform transform,float x,float y,float z)
    {
        TmpV3A.Set(x, y, z);
        transform.position = TmpV3A;
    }
    #endregion

    #region SetLocalPosition
    public static void SetLocalPosition(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.localPosition = TmpV3A;
    }
    #endregion

    #region SetEulerAngles

    public static void SetEulerAngles(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.eulerAngles = TmpV3A;
    }

    public static void SetEulerAnglesZ(this Transform transform, float z)
    {
        transform.SetEulerAngles(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
    #endregion

    #region AddEulerAngles

    public static void AddEulerAnglesZ(this Transform transform, float z)
    {
        transform.SetEulerAnglesZ(transform.eulerAngles.z + z);
    }
    #endregion
}
