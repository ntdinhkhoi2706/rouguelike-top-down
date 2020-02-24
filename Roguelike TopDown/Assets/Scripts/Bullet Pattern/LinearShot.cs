using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : BaseShot
{
    [SerializeField]
    private float betweenDelay=0;
    [Range(0f,360f)]
    public float angle=0;
    protected override void Awake()
    {

        base.Awake();
    }

    public override void Shot()
    {
        StartCoroutine(ShotCoroutine());
        base.Shot();
    }
    IEnumerator ShotCoroutine()
    {
        if (bulletNum <= 0 || bulletSpeed <= 0)
            yield break;

        if (Shooting)
            yield break;

        Shooting = true;

        for(int i=0;i<BulletNum;i++)
        {
            if(0<i || 0<betweenDelay)
            {
                yield return StartCoroutine(MyUtil.WaitForSeconds(betweenDelay));
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (!bullet)
                break;
            if(!setAngle)
            {
                angle = gunHand.transform.rotation.eulerAngles.z;
            }
            
            ShotBullet(bullet, bulletSpeed,angle);
            AutoReleaseBulletGameObject(bullet.gameObject);
        }

        FinishShot();
    }
}
