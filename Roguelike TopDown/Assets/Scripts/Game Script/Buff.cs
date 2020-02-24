using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buff : MonoBehaviour
{
    public MainBuff buff;
    [SerializeField]
    private Text buffText=null;

    public SpriteRenderer sr=null;

    
    private bool canTakeBuff;

    void Update()
    {
        if(JoyStickCanvas.Instance)
        {
            if(JoyStickCanvas.Instance.activity &&  canTakeBuff)
            {
                UIController.Instance.buffImg.sprite = buff.sprite;
                UIController.Instance.buffImg.GetComponent<Animator>().SetTrigger("Do");
                GameManager.Instance.mainBuff.Add(buff);

                foreach(var buff in GameManager.Instance.mainBuff)
                {
                    if(buff.nameBuff.Contains("IncreaseHealth"))
                    {
                        PlayerHealthController.Instance.InCreaseMaxHealth(4);
                    }else if(buff.nameBuff.Contains("ReduceSkill"))
                    {
                        PlayerController.Instance.timeCoolDown -= 1;
                    }
                }

                var shop = GetComponentInParent<BuffShopItem>();
                foreach(var b in shop.buffs)
                {
                    if(b!=this)
                    {
                        GameManager.Instance.containBuff.Add(b.buff);
                    }
                }
                Destroy(shop.gameObject);
                JoyStickCanvas.Instance.activity = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            canTakeBuff = true;
            buffText.gameObject.SetActive(true);
            buffText.text = buff.infoBuff;
            JoyStickCanvas.Instance.ChangeShotToActivity();
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            canTakeBuff = false;
            buffText.gameObject.SetActive(false);
            JoyStickCanvas.Instance.ChangeActivityToShot();
        }
    }
}

