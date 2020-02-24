using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI : MonoBehaviour
{
    public Transform itemsParent = null;

    public CraftSlot slots = null;

    public GameObject craftPanel = null;
    void OnEnable()
    {
        if (CraftInventory.Instance.items.Count > 0)
        {
            for (int i = 0; i < CraftInventory.Instance.items.Count; i++)
            {
                var slot = Instantiate(slots, itemsParent);
                slot.icon.sprite = CraftInventory.Instance.items[i].weapon.GetComponent<SpriteRenderer>().sprite;
                slot.nameGun.text = CraftInventory.Instance.items[i].weapon.theGun.gunName;
                slot.icon.gameObject.SetActive(true);
                slot.gun = CraftInventory.Instance.items[i].weapon;
                slot.needs = CraftInventory.Instance.items[i].needed;

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
