using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum Type
    {
        Normal,
        Debuff,
        SpawnAtStart,
        DuringShot
    }
    [Header("Fx")]
    [SerializeField]
    private GameObject bulletImpact=null;

    [Header("Debuff")]
    public Type type;
    [SerializeField]
    private float repeatTime=0;
    [SerializeField]
    private GameObject debuff=null;

    [Header("Bullet Ability")]
    [SerializeField]
    private GameObject spawnBullet=null;
    
    void OnEnable()
    {
        if (type==Type.DuringShot)
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
        if(target.tag == "Player")
        {
            PlayerHealthController.Instance.DamagePlayer();
            if (type==Type.Debuff)
            {
                var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
                debuff1.GetComponent<Debuff>().Apply(target);
            }
            

        }
        ActivateEffect();
    }

    void ActivateEffect()
    {
        if (type==Type.SpawnAtStart)
        {
            ObjectPooling.Instance.GetGameObject(spawnBullet, transform.position, Quaternion.identity);
        }
        
        ObjectPooling.Instance.GetGameObject(bulletImpact, transform.position, Quaternion.identity);
    }
    
}
