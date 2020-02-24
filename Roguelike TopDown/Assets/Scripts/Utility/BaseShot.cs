using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int bulletNum = 10;

    public float bulletSpeed = 2f;

    public float accelerationSpeed = 0f;

    public float accelerationTurn = 0f;

    public bool autoReleased = false;

    public float autoReleaseTime = 10f;

    protected bool Shooting;

    public Transform gunHand { get; set; }

    public int BulletNum { get => bulletNum; set => bulletNum = value; }

    private int startBulletNum;
    public bool setAngle;
    protected virtual void Awake()
    {
        if(!setAngle)
        {
            gunHand = GetComponentInParent<GunHand>().transform;
        }
        
            var goBulletList = new List<GameObject>();
            for (int i = 0; i < BulletNum; i++)
            {
                var bullet = GetBullet(Vector3.zero, Quaternion.identity, true);
                if (bullet != null)
                {
                   goBulletList.Add(bullet.gameObject);
               }
            }

            for (int i = 0; i < goBulletList.Count; i++)
            {
                ObjectPooling.Instance.ReleaseGameObject(goBulletList[i]);
            }
        

        if(gameObject.GetComponentInParent<EnemyController>())
        {
            if(GameManager.Instance.mainBuff.Count>0)
            {
                foreach(var buff in GameManager.Instance.mainBuff)
                {
                    if(buff.nameBuff.Contains("DecreaseEnemyBullet"))
                    {
                        bulletSpeed -= 0.5f;
                    }
                }
            }
        }

        startBulletNum = BulletNum;
    }

    protected virtual void OnDisable()
    {
        Shooting = false;
    }

    public virtual void Shot()
    {
        if (gameObject.GetComponentInParent<Gun>() )
        {
            if(gameObject.GetComponentInParent<Gun>().nameWeapon == NameWeapon.Shotgun)
            {
                if (GameManager.Instance.mainBuff.Count > 0)
                {
                    foreach (var buff in GameManager.Instance.mainBuff)
                    {
                        if (buff.nameBuff.Contains("IncreaseNumberOfShotgun"))
                        {

                            bulletNum += 3;
                        }
                    }
                }
            }
        }
    }

    protected void FinishShot()
    {
        bulletNum = startBulletNum;
        Shooting = false;
    }



    protected Bullet GetBullet(Vector3 position,Quaternion rotation,bool forceInstantiate=false)
    {
        if(bulletPrefab ==  null)
        {
            return null;
        }
        var goBullet = ObjectPooling.Instance.GetGameObject(bulletPrefab, position, rotation, forceInstantiate);
        
        
        
        if (goBullet == null)
            return null;

        var bullet = goBullet.GetComponent<Bullet>();
        if(bullet == null)
        {
            bullet = goBullet.AddComponent<Bullet>();
        }
        return bullet;
    }


    protected void ShotBullet(Bullet bullet,float speed,float angle=0)
    {
        if (bullet == null)
            return;
        bullet.Shot(speed, angle, accelerationSpeed, accelerationTurn);
    }


    protected void AutoReleaseBulletGameObject(GameObject goBullet)
    {
        if (autoReleased == false || autoReleaseTime < 0f)
        {
            return;
        }
        StartCoroutine(AutoReleaseBulletGameObjectCoroutine(goBullet));
    }


    IEnumerator AutoReleaseBulletGameObjectCoroutine(GameObject goBullet)
    {
        float countUpTime = 0f;

        while (true)
        {
            if (goBullet == null || goBullet.activeInHierarchy == false)
            {
                yield break;
            }

            if (autoReleaseTime <= countUpTime)
            {
                break;
            }

            yield return 0;

            countUpTime += Time.deltaTime;
        }
        ObjectPooling.Instance.ReleaseGameObject(goBullet);
    }



}
