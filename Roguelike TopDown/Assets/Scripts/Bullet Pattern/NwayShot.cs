using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NwayShot : BaseShot
{
    public int wayNum = 5;

    [Range(0f, 360f)]
    public float betweenAngle = 10f;

    public float nextLineDelay = 0.1f;

    [Range(0f,360f)]
    public float setCenterAngle=0;

    private float centerAngle;
    protected override void Awake()
    {
        
        base.Awake();
    }

    public override void Shot()
    {
        base.Shot();
        StartCoroutine(ShotCoroutine());
        
    }

    IEnumerator ShotCoroutine()
    {
        if (BulletNum <= 0 || bulletSpeed <= 0f || wayNum <= 0)
            yield break;

        if (Shooting)
            yield break;

        Shooting = true;

        int wayIndex = 0;

        for(int i=0;i<BulletNum;i++)
        {
            if(wayNum <= wayIndex)
            {
                wayIndex = 0;

                if(0f<nextLineDelay)
                {
                    yield return StartCoroutine(MyUtil.WaitForSeconds(nextLineDelay));
                }
            }

            var bullet = GetBullet(transform.position, transform.rotation);

            if (bullet == null)
                break;

            if(setAngle)
            {
                centerAngle = setCenterAngle;
            }else
            {
                centerAngle = gunHand.transform.rotation.eulerAngles.z;
            }
            
            

            float baseAngle = wayNum % 2 == 0 ? centerAngle - (betweenAngle / 2f) : centerAngle;

            float angle = MyUtil.GetShiftedAngle(wayIndex, baseAngle, betweenAngle);

            ShotBullet(bullet, bulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);

            wayIndex++;
                
        }

        FinishShot();
    }
}
