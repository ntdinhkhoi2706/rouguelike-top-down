using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : TimeEffect
{
    public int damageToEnemy;
    public int damageToBoss;

    protected override void ApplyEfx(Collider2D target)
    {
        if(target.gameObject.activeInHierarchy)
        {

            switch (type)
            {
                case Type.Poison:
                    if(target.GetComponent<EnemyController>())
                    {
                        var enemy = target.GetComponent<EnemyController>();
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (buff.nameBuff.Contains("ImmunePoison"))
                                {
                                    enemy.DealDamage(damageToEnemy*2);
                                    enemy.poison.SetActive(true);
                                }
                            }
                        }else
                        {
                            enemy.DealDamage(damageToEnemy);
                            enemy.poison.SetActive(true);
                        }
                    }else if(target.GetComponent<BossController>())
                    {
                        var boss = target.GetComponent<BossController>();
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (buff.nameBuff.Contains("ImmunePoison"))
                                {
                                    boss.BossTakeDamage(damageToBoss * 2);
                                    boss.poison.SetActive(true);
                                }
                            }
                        }
                        else
                        {
                            boss.BossTakeDamage(damageToBoss);
                            boss.poison.SetActive(true);
                        }
                    }
                    else if(target.GetComponent<PlayerController>())
                    {
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (!buff.nameBuff.Contains("ImmunePoison"))
                                {
                                    PlayerHealthController.Instance.DamagePlayer();
                                    PlayerController.Instance.debuffPoison.SetActive(true);
                                }
                            }
                        }
                        else
                        {
                            PlayerHealthController.Instance.DamagePlayer();
                            PlayerController.Instance.debuffPoison.SetActive(true);
                        }
                    }
                    
                    break;
                case Type.Fire:
                    if (target.GetComponent<EnemyController>())
                    {
                        var enemy = target.GetComponent<EnemyController>();
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (buff.nameBuff.Contains("ImmuneFire"))
                                {
                                    enemy.DealDamage(damageToEnemy * 2);
                                    enemy.fire.SetActive(true);
                                }
                            }
                        }
                        else
                        {
                            enemy.DealDamage(damageToEnemy);
                            enemy.fire.SetActive(true);
                        }
                    }
                    else if (target.GetComponent<BossController>())
                    {
                        var boss = target.GetComponent<BossController>();
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (buff.nameBuff.Contains("ImmuneFire"))
                                {
                                    boss.BossTakeDamage(damageToBoss * 2);
                                    boss.fire.SetActive(true);
                                }
                            }
                        }
                        else
                        {
                            boss.BossTakeDamage(damageToBoss);
                            boss.fire.SetActive(true);
                        }
                    }
                    else if(target.GetComponent<PlayerController>())
                    {
                        if (GameManager.Instance.mainBuff.Count > 0)
                        {
                            foreach (var buff in GameManager.Instance.mainBuff)
                            {
                                if (!buff.nameBuff.Contains("ImmuneFire"))
                                {
                                    PlayerHealthController.Instance.DamagePlayer();
                                    PlayerController.Instance.debuffFire.SetActive(true);
                                }
                            }

                        }
                        else
                        {
                            PlayerHealthController.Instance.DamagePlayer();
                            PlayerController.Instance.debuffFire.SetActive(true);
                        }
                    }
                    break;
                case Type.Ice:
                    if(target.GetComponent<EnemyController>())
                    {
                        target.GetComponent<EnemyController>().ice.SetActive(true);
                        target.GetComponent<EnemyController>().canMove = false;
                    }else if (target.GetComponent<BossController>())
                    {
                        target.GetComponent<BossController>().ice.SetActive(true);
                        target.GetComponent<BossController>().canMove = false;
                    }else if(target.GetComponent<PlayerController>())
                    {
                        PlayerController.Instance.CanMove = false;
                        PlayerController.Instance.debuffIce.SetActive(true);
                    }
                    break;
                case Type.Health:
                    if(target.GetComponent<PlayerController>())
                    {
                        PlayerHealthController.Instance.HealthPlayer(1);
                    }
                    break;

            }

        }else
        {
            gameObject.SetActive(false);
        }
            
    }

    protected override void EndEfx(Collider2D target)
    {
        if(target.GetComponent<EnemyController>())
        {
            var enemy = target.GetComponent<EnemyController>();
            enemy.poison.SetActive(false);
            enemy.fire.SetActive(false);
            enemy.ice.SetActive(false);
            enemy.canMove = true;
        }else if(target.GetComponent<BossController>())
        {
            var boss = target.GetComponent<BossController>();
            boss.poison.SetActive(false);
            boss.fire.SetActive(false);
            boss.ice.SetActive(false);
            boss.canMove = true;
        }else if(target.GetComponent<PlayerController>())
        {
            PlayerController.Instance.debuffPoison.SetActive(false);
            PlayerController.Instance.debuffFire.SetActive(false);
            PlayerController.Instance.debuffIce.SetActive(false);
            PlayerController.Instance.CanMove = true;
        }
        
    }




}
