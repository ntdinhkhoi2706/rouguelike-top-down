﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static IEnumerator WaitForSeconds(float waitTime)
    {
        float elapsedTime = 0f;
        while(elapsedTime < waitTime)
        {
            elapsedTime += Timer.Instance.DeltaTime;
            yield return 0;
        }
    }

    public static Transform GetTransformFromTagName(string tagName)
    {
        if(string.IsNullOrEmpty(tagName))
        {
            return null;
        }

        GameObject goTarget = GameObject.FindWithTag(tagName);
        if(!goTarget)
        {
            return null;
        }
        return goTarget.transform;
    }


    public static float GetShiftedAngle(int wayIndex,float baseAngle,float betweenAngle)
    {
        float angle = wayIndex % 2 == 0 ?
            baseAngle - (betweenAngle * (float)wayIndex / 2f):
            baseAngle + (betweenAngle * Mathf.Ceil((float)wayIndex / 2f));
        return angle;
    }

    public static float Get360Angle(float angle)
    {
        while(angle <0f)
        {
            angle += 360f;
        }
        while(360f <angle)
        {
            angle -= 360f;
        }
        return angle;
    }

    public static float GetZAngleFromTwoPosition(Transform fromTrans,Transform toTrans)
    {
        if(fromTrans == null || toTrans==null)
        {
            return 0f;
        }

        var xDistance = toTrans.position.x - fromTrans.position.x;
        var yDistance = toTrans.position.y - fromTrans.position.y;
        var angle = Mathf.Atan2(xDistance, yDistance) * Mathf.Rad2Deg;
        angle = -Get360Angle(angle);
        return angle;
    }
}