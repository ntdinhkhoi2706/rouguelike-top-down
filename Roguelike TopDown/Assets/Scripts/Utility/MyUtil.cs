using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static IEnumerator WaitForSeconds(float waitTime)
    {
        float elapsedTime = 0f;
        while(elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
    }

    public static float GetShiftedAngle(int wayIndex,float baseAngle,float betweenAngle)
    {
        float angle = wayIndex % 2 == 0 ?
            baseAngle - (betweenAngle * (float)wayIndex / 2f):
            baseAngle + (betweenAngle * Mathf.Ceil((float)wayIndex / 2f));
        return angle;
    }

}
