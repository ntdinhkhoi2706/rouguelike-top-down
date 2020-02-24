using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeEffect : MonoBehaviour
{
    public enum Type
    {
        Poison,
        Fire,
        Ice,
        Health
    }
    public float duration=0;
    public float repeatTime=0;
    private int applyTime = 0;


    public Type type;

    public  void Apply(Collider2D target)
    {
            StartCoroutine(Debuff(repeatTime, duration,target));
    }

    IEnumerator Debuff(float repeatTime,float duration,Collider2D target)
    {
        while(applyTime<duration)
        {
            if(!target.gameObject.activeInHierarchy)
            {
                break;
            }else if (target.GetComponent<PlayerController>())
            {
                if (PlayerHealthController.Instance.CurrentArmor <= 0)
                {
                    if (GameManager.Instance.mainBuff.Count > 0)
                    {
                        foreach (var buff in GameManager.Instance.containBuff)
                        {
                            if (buff.nameBuff.Contains("NoExtraDamage"))
                            {
                                EndEfx(target);
                                applyTime = 0;
                                gameObject.SetActive(false);
                                yield break;
                            }
                        }
                    }
                }
            }
            ApplyEfx(target);
            yield return StartCoroutine(MyUtil.WaitForSeconds(repeatTime));
            applyTime++;
        }
        EndEfx(target);
        applyTime = 0;
        gameObject.SetActive(false);
    }
    protected virtual void ApplyEfx(Collider2D target)
    {
      
    }

    protected virtual void EndEfx(Collider2D target)
    {

    }

    
}
