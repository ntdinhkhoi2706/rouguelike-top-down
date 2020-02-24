using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
    static Vector3 TmpV3A = Vector3.zero;
    static Vector3 TmpV3B = Vector3.zero;
    static Vector2 TmpV2 = Vector2.zero;

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

    public static void SetPosition(this Transform transform,float x,float y)
    {
        transform.SetPosition(x, y, transform.position.z);
    }

    public static void SetPositionX(this Transform transform,float x)
    {
        transform.SetPosition(x, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        transform.SetPosition(transform.position.x, y, transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.SetPosition(transform.position.x, transform.position.y,z);
    }
    #endregion

    #region SetLocalPosition
    public static void SetLocalPosition(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.localPosition = TmpV3A;
    }

    public static void SetLocalPosition(this Transform transform, float x, float y)
    {
        transform.SetLocalPosition(x, y, transform.localPosition.z);
    }

    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.SetLocalPosition(x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.SetLocalPosition(transform.localPosition.x, y, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.SetLocalPosition(transform.localPosition.x, transform.localPosition.y, z);
    }
    #endregion

    #region SetEulerAngles

    public static void SetEulerAngles(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.eulerAngles = TmpV3A;
    }

    public static void SetEulerAnglesX(this Transform transform, float x)
    {
        transform.SetEulerAngles(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesY(this Transform transform, float y)
    {
        transform.SetEulerAngles(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesZ(this Transform transform, float z)
    {
        transform.SetEulerAngles(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
    #endregion

    #region Find

    public static Transform FindInChildren(this Transform self, string name)
    {
        int count = self.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = self.GetChild(i);
            if (child.name == name) return child;
            Transform subChild = child.FindInChildren(name);
            if (subChild != null) return subChild;
        }
        return null;
    }

    #endregion

    #region LookAt
    public static void LookAt2D(this Transform transform, Vector2 target, Vector3 axis, float angle)
    {
        TmpV2.Set(target.x - transform.position.x, target.y - transform.position.y);
        angle = angle + Mathf.Atan2(TmpV2.y, TmpV2.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, axis);
    }
    #endregion

    #region SmoothStepLocalPositon
    public static void SmoothStepLocalPosition(this Transform transform, Vector3 to, float t)
    {
        var newX = Mathf.SmoothStep(transform.localPosition.x, to.x, t);
        var newY = Mathf.SmoothStep(transform.localPosition.y, to.y, t);
        var newZ = Mathf.SmoothStep(transform.localPosition.z, to.z, t);
        transform.SetLocalPosition(newX, newY, newZ);
    }

    public static void SmoothStepLocalPosition(this Transform transform, Vector3 from, Vector3 to, float t)
    {
        var newX = Mathf.SmoothStep(from.x, to.x, t);
        var newY = Mathf.SmoothStep(from.y, to.y, t);
        var newZ = Mathf.SmoothStep(from.z, to.z, t);
        transform.SetLocalPosition(newX, newY, newZ);
    }
    #endregion

    #region SmoothStepPosition

    public static void SmoothStepPosition(this Transform transform, Vector3 to, float t)
    {
        var newX = Mathf.SmoothStep(transform.position.x, to.x, t);
        var newY = Mathf.SmoothStep(transform.position.y, to.y, t);
        var newZ = Mathf.SmoothStep(transform.position.z, to.z, t);
        transform.SetPosition(newX, newY, newZ);
    }

    public static void SmoothStepPosition(this Transform transform, Vector3 from, Vector3 to, float t)
    {
        var newX = Mathf.SmoothStep(from.x, to.x, t);
        var newY = Mathf.SmoothStep(from.y, to.y, t);
        var newZ = Mathf.SmoothStep(from.z, to.z, t);
        transform.SetPosition(newX, newY, newZ);
    }
    #endregion

    #region SlerpLocalRotate

    public static void SlerpLocalRotate(this Transform transform, Quaternion to, float t)
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, to, t);
    }
    public static void SlerpLocalRotate(this Transform transform, Quaternion from, Quaternion to, float t)
    {
        transform.localRotation = Quaternion.Slerp(from, to, t);
    }
    #endregion

    #region SlerpRotate

    public static void SlerpRotate(this Transform transform, Quaternion to, float t)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, to, t);
    }
    public static void SlerpRotate(this Transform transform, Quaternion from, Quaternion to, float t)
    {
        transform.rotation = Quaternion.Slerp(from, to, t);
    }
    #endregion

    #region LerpLocalRotate

    public static void LerpLocalRotate(this Transform transform, Quaternion to, float t)
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, to, t);
    }
    public static void LerpLocalRotate(this Transform transform, Quaternion from, Quaternion to, float t)
    {
        transform.localRotation = Quaternion.Lerp(from, to, t);
    }
    #endregion

    #region LerpRotate

    public static void LerpRotate(this Transform transform, Quaternion to, float t)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, to, t);
    }
    public static void LerpRotate(this Transform transform, Quaternion from, Quaternion to, float t)
    {
        transform.rotation = Quaternion.Lerp(from, to, t);
    }
    #endregion

    #region AddEulerAngles
    public static void AddEulerAnglesY(this Transform transform, float y)
    {
        transform.SetEulerAnglesY(transform.eulerAngles.y + y);
    }

    public static void AddEulerAnglesZ(this Transform transform, float z)
    {
        transform.SetEulerAnglesZ(transform.eulerAngles.z + z);
    }
    #endregion

    #region SetLocalScale

    public static void SetLocalScale(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.localScale = TmpV3A;
    }

    public static void SetLocalScaleX(this Transform transform, float x)
    {
        transform.SetLocalScale(x, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float y)
    {
        transform.SetLocalScale(transform.localScale.x, y, transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform transform, float z)
    {
        transform.SetLocalScale(transform.localScale.x, transform.localScale.y, z);
    }
    #endregion

    #region SetLocalEulerAngles

    public static void SetLocalEulerAngles(this Transform transform, float x, float y, float z)
    {
        TmpV3A.Set(x, y, z);
        transform.localEulerAngles = TmpV3A;
    }

    public static void SetLocalEulerAnglesX(this Transform transform, float x)
    {
        transform.SetLocalEulerAngles(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesY(this Transform transform, float y)
    {
        transform.SetLocalEulerAngles(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesZ(this Transform transform, float z)
    {
        transform.SetLocalEulerAngles(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
    }

    #endregion
}
