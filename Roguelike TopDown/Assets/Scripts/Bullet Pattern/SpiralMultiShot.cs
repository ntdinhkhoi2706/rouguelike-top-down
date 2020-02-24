using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMultiShot : BaseShot
{
    public int spiralWayNum = 4;

    [Range(0f, 360f)]
    public float startAngle = 180f;

    [Range(-360f, 360f)]
    public float shiftAngle = 5f;

    public float betweenDelay = 0.2f;

    protected override void Awake()
    {
       
        base.Awake();
    }

    public override void Shot()
    {
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        if (BulletNum <= 0 || bulletSpeed <= 0f || spiralWayNum <= 0)
            yield break;

        if (Shooting)
            yield break;

        Shooting = true;

        float spiralWayShiftAngle = 360f / spiralWayNum;

        int spiralWayIndex = 0;

        for(int i=0;i<BulletNum;i++)
        {
            if(spiralWayNum <= spiralWayIndex)
            {
                spiralWayIndex = 0;
                if(0f < betweenDelay)
                {
                    yield return StartCoroutine(MyUtil.WaitForSeconds(betweenDelay));
                }
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null)
                break;

            float angle = startAngle + (spiralWayShiftAngle * spiralWayIndex) + (shiftAngle * Mathf.Floor(i / spiralWayNum));

            ShotBullet(bullet, bulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);

            spiralWayIndex++;
        }

        FinishShot();
    }
}
