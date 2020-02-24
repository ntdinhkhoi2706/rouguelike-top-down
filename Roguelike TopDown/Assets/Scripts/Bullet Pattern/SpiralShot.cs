using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShot : BaseShot
{
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
        if(BulletNum <=0 || bulletSpeed <=0f)
        {
            yield break;
        }

        if (Shooting)
            yield break;

        Shooting = true;

        for(int i=0;i<BulletNum;i++)
        {
            if(0 <i && 0f < betweenDelay)
            {
                yield return StartCoroutine(MyUtil.WaitForSeconds(betweenDelay));
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null)
                break;

            
            float angle = startAngle + (shiftAngle * i);

            ShotBullet(bullet, bulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);
        }

        FinishShot();
    }
}
