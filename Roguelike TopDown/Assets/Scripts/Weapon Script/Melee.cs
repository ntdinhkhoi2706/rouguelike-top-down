using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    public enum Type
    {
        Normal,
        AddForce
    }
    private Animator anim;
    [SerializeField]
    private Transform attackPoint = null;
    [SerializeField]
    private float attackRange = 0.5f;
    [SerializeField]
    private LayerMask enemyLayer=1;

    [SerializeField]
    private int count=0;

    public Type type;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void ProcessAttack()
    {
        StartCoroutine(AttackCoroutine());
        base.ProcessAttack();
    }

    IEnumerator AttackCoroutine()
    {
        for (int i = 0; i < count; i++)
        {
            if (0 < i)
            {
               
                yield return StartCoroutine(MyUtil.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).speed-0.6f));
            }
            if(type == Type.AddForce)
            {
                PlayerController.Instance.rb.AddForce(transform.right * 50f,ForceMode2D.Impulse);
            }
            anim.SetTrigger("Attack");

            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hit)
            {
                if (enemy.GetComponent<Bullet>())
                {
                    enemy.gameObject.SetActive(false);
                }
                if (enemy.GetComponent<EnemyController>())
                {
                    enemy.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                }
                else if (enemy.GetComponent<BossController>())
                {
                    enemy.GetComponent<BossController>().BossTakeDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                }

            }

        }

        
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
