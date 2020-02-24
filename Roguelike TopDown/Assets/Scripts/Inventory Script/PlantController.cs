using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public int slot=0;
    public string namePlant = null;

    public bool HaveItem
    {
        get => PlayerPrefs.GetInt(name+"HaveItem", 0) == 1;
        set => PlayerPrefs.SetInt(name+"HaveItem", value ? 1 : 0);
    }

    void Start()
    {
        if(HaveItem)
        { 
            foreach (var reward in PlantInventory.Instance.itemsPickup)
            {
                    bool check = reward.item.reward.CheckIDSlot(namePlant, slot);
                    if (check)
                    {
                        Instantiate(reward.item.reward, transform);

                    }
                
            }
        }
       
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        }
    }

}
