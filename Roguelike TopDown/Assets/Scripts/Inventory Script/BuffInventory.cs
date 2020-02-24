using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInventory : MonoBehaviour
{
    public BuffSlot slot=null;
    void OnEnable()
    {
        if(GameManager.Instance.mainBuff.Count>0)
        {
            foreach (var buff in GameManager.Instance.mainBuff)
            {
                var buffSlot = Instantiate(slot, transform);
                buffSlot.GetComponent<BuffSlot>();
                buffSlot.icon.sprite = buff.sprite;
            }
        }
        
    }

    void OnDisable()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
       
    }
}
