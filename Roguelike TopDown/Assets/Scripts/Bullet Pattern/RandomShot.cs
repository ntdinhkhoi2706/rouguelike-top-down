using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShot : BaseShot
{
    [Range(0f, 360f)]
    public float randomCenterAngle = 180f;

    [Range(0f, 360f)]
    public float rangdomRangeSize = 360f;

    public float randomSpeedMin = 1f;

    public float randomSpeedMax = 3f;

    public float randomDelayMin = 0.01f;

    public float randomDelayMax = 0.1f;


    public bool rotate=true;

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
        if (BulletNum <= 0 || randomSpeedMin <= 0f || randomSpeedMax <= 0)
        {
            yield break;
        }
        if (Shooting)
        {
            yield break;
        }
        Shooting = true;

        List<int> numList = new List<int>();

        for (int i = 0; i < BulletNum; i++)
        {
            numList.Add(i);
        }

        while (0 < numList.Count)
        {
            int index = Random.Range(0, numList.Count);
            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null)
            {
                break;
            }

            float bulletSpeed = Random.Range(randomSpeedMin, randomSpeedMax);

            if (rotate)
            {
                randomCenterAngle = gunHand.transform.rotation.eulerAngles.z;
            }

            float minAngle = randomCenterAngle - (rangdomRangeSize / 2f);
            float maxAngle = randomCenterAngle + (rangdomRangeSize / 2f);
            float angle = 0f;
            float oneDirectionNum = Mathf.Floor((float)BulletNum / 4f);
            float quarterIndex = Mathf.Floor((float)numList[index] / oneDirectionNum);
            float quarterAngle = Mathf.Abs(maxAngle - minAngle) / 4f;
            angle = Random.Range(minAngle + (quarterAngle * quarterIndex), minAngle + (quarterAngle * (quarterIndex + 1f)));


            ShotBullet(bullet, bulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);

            numList.RemoveAt(index);

            if (0 < numList.Count && 0f <= randomDelayMin && 0f < randomDelayMax)
            {
                float waitTime = Random.Range(randomDelayMin, randomDelayMax);
                yield return StartCoroutine(MyUtil.WaitForSeconds(waitTime));
            }
        }

        FinishShot();
    }
}
