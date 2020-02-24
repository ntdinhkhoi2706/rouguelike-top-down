using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInventoryUI : MonoBehaviour
{
    public Transform itemsParent = null;


    public PlantSlot slots = null;

    void OnEnable()
    {
        if (PlantInventory.Instance.items.Count > 0)
        {
            for (int i = 0; i < PlantInventory.Instance.items.Count; i++)
            { 
                var slot = Instantiate(slots, itemsParent);
                slot.icon.sprite = PlantInventory.Instance.items[i].icon;
                slot.countText.text = PlantInventory.Instance.items[i].Count.ToString();
                slot.reward = PlantInventory.Instance.items[i].reward;
                slot.icon.gameObject.SetActive(true);

            }
        }
    }

    public void CloseInventory()
    {
        JoyStickCanvas.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
        foreach (Transform item in itemsParent)
        {
            Destroy(item.gameObject);
        }
    }
}
