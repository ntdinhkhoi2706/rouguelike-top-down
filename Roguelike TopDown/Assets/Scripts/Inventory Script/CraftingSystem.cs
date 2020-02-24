using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;
    [HideInInspector]
    public List<MaterialNeeded> neededs;
    public GameObject shopMain=null, shopPart=null;
    public GunPickup weapon { get; set; }

    
    public Text damage = null, crit = null, mana = null, defection = null, nameWeapon = null;
    public Image icon = null;
    void Awake()
    {
        Instance = this;
    } 

    bool CheckCraft()
    {
        int count = 0;
        foreach (var item in Inventory.Instance.items)
        {
            foreach (var need in neededs)
            {  
                if (item.Count >= need.count && item.name == need.item.GetComponent<MaterialNeed>().item.name)
                {
                    count++;
                }
            }
        }
        return count == neededs.Count ;
    }

    public void UpdateUI(GunPickup gun)
    {
        damage.text = gun.theGun.defaultConfig.damage.ToString();
        crit.text = gun.theGun.defaultConfig.critChance.ToString();
        mana.text = gun.theGun.defaultConfig.mana.ToString();
        defection.text = gun.theGun.defaultConfig.deflection.ToString();
        nameWeapon.text = gun.theGun.gunName;
        icon.sprite = gun.GetComponent<SpriteRenderer>().sprite;
        weapon = gun;
    }

    public void Crafting()
    {
        var canCraft = CheckCraft();
        if(canCraft)
        {
            Instantiate(weapon, PlayerController.Instance.transform.position, Quaternion.identity);
            foreach (var item in Inventory.Instance.items)
            {
                foreach (var need in neededs)
                {
                    if (item.Count >= need.count && item.name == need.item.GetComponent<MaterialNeed>().item.name)
                    {
                        item.Count -= need.count;
                            Inventory.Instance.Remove(item);
                    }
                }
            }
            JoyStickCanvas.Instance.gameObject.SetActive(true);
            shopPart.SetActive(false);
            shopMain.SetActive(false);
        }
    }

    public void CloseCraft()
    {
        Transform parent = GameObject.Find("Material Panel").transform;
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }
        gameObject.SetActive(false);
        
    }
}
