using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum Type
    {
        Normal,
        Debuff,
        SpawnZone
    }
    [Header("Fx")]
    [SerializeField]
    private GameObject bulletImpact=null;

    [Header("Debuff")]
    public Type type;
    [SerializeField]
    private GameObject zone=null;
    [SerializeField]
    private GameObject debuff=null;

    [Header("Bullet Ability")]
    [SerializeField]
    private float repeatTime=0;
    [SerializeField]
    private bool spawnAtStart=false;
    [SerializeField]
    private bool spawnDuringShot=false;
    [SerializeField]
    private GameObject spawnBullet=null;

    
    void OnEnable()
    {
        if(spawnDuringShot)
        {
            if (repeatTime > 0)
            {
                InvokeRepeating("SpawnDuringShot", 0.1f, repeatTime);
            }
            else
                Invoke("SpawnDuringShot", 0.1f);  
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void SpawnDuringShot()
    {
        ObjectPooling.Instance.GetGameObject(spawnBullet, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        ActivateEffect();
        AudioManager.Instance.PlaySFX(3);
        if (target.tag == "Enemy")
        {
            if (target.GetComponent<EnemyController>())
            {
                    int rd = Random.Range(0, 100);
                    if (rd < WeaponManager.Instance.current_Weapon.defaultConfig.critChance)
                    {
                        switch (type)
                        {
                            case Type.Normal:
                                target.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage * 2);
                                break;
                            case Type.Debuff:
                                var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
                                debuff1.GetComponent<Debuff>().Apply(target);
                                break;
                            case Type.SpawnZone:
                                Vector3 tmp = transform.position;
                                tmp.x += Random.Range(-1f, 1f);
                                tmp.y += Random.Range(-1f, 1f);
                                ObjectPooling.Instance.GetGameObject(zone, tmp, Quaternion.identity);
                                break;
                        }

                    }else
                    {
                        target.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                    }
            }
            else if (target.GetComponent<BossController>())
            {
                    int rd = Random.Range(0, 100);
                    if (rd < WeaponManager.Instance.current_Weapon.defaultConfig.critChance)
                    {
                        switch (type)
                        {
                            case Type.Normal:
                                target.GetComponent<BossController>().BossTakeDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage * 2);
                                break;
                            case Type.Debuff:
                                var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
                                debuff1.GetComponent<Debuff>().Apply(target);
                                break;
                            case Type.SpawnZone:
                                Vector3 tmp = transform.position;
                                tmp.x += Random.Range(-1f, 1f);
                                tmp.y += Random.Range(-1f, 1f);
                                ObjectPooling.Instance.GetGameObject(zone, tmp, Quaternion.identity);
                                break;
                        }

                    }else
                    {
                        target.GetComponent<BossController>().BossTakeDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                    }
            }

        }

    }
    void ActivateEffect()
    {
        if (spawnAtStart)
        {
            ObjectPooling.Instance.GetGameObject(spawnBullet, transform.position, Quaternion.identity);
        }

        ObjectPooling.Instance.GetGameObject(bulletImpact, transform.position, Quaternion.identity);
    }
}
