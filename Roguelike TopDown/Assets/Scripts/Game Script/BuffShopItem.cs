using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShopItem : MonoBehaviour
{   

    public List<Buff> buffs=new List<Buff>();


    void Start()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            int rd = Random.Range(0, GameManager.Instance.containBuff.Count);
            buffs[i].buff = GameManager.Instance.containBuff[rd];
            buffs[i].sr.sprite = GameManager.Instance.containBuff[rd].sprite;
            GameManager.Instance.containBuff.RemoveAt(rd);
        }
    }
        
    
    
     
}
