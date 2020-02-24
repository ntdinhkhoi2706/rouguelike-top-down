using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNote : WeaponController
{
    [SerializeField]
    private GameObject deathFx = null, deathText = null;

    private Animator anim;
    GameObject dead;

    void Start()
    {
        anim = GetComponent<Animator>();
        var tmp = PlayerController.Instance.transform.position;
        tmp.y += 1f;
        dead= Instantiate(deathText, tmp,Quaternion.identity,PlayerController.Instance.transform);
        
    }


    public override void ProcessAttack()
    {
        if(PlayerController.Instance.Enemy)
        {
                dead.GetComponent<Animator>().SetTrigger("Attack");
                anim.SetTrigger("Attack");
                base.ProcessAttack();
        } 
    }

    void KillEnemy()
    {
        if(PlayerController.Instance.Enemy)
        {
            ObjectPooling.Instance.GetGameObject(deathFx, PlayerController.Instance.Enemy.transform.position, Quaternion.identity);
            PlayerController.Instance.Enemy.GetComponent<EnemyController>().DealDamage(defaultConfig.damage);
        }
        
        
    }
}
