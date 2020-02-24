using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    
    public InventorySlot slots = null;

    void OnEnable()
    {
       if(Inventory.Instance.items.Count>0)
       {
         for(int i=0;i<Inventory.Instance.items.Count;i++)
         {
            if (Inventory.Instance.items[i].HaveItem)
            {
                   
                        var slot = Instantiate(slots, itemsParent);
                        slot.icon.sprite = Inventory.Instance.items[i].icon;
                        slot.countText.text = Inventory.Instance.items[i].Count.ToString();
                        slot.icon.gameObject.SetActive(true); 
            }
         } 
       }
    }

    public void CloseInventory()
    {
        JoyStickCanvas.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
        foreach(Transform item in itemsParent)
        {
            Destroy(item.gameObject);
        }
    }

}
