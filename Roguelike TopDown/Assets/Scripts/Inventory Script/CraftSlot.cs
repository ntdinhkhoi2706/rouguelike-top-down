using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public GunPickup gun { get; set; }
    public Image icon = null;
    public Text nameGun = null;

    public List<MaterialNeeded> needs = new List<MaterialNeeded>();

    public void OpenMaterialNeed()
    {
        GetComponentInParent<CraftUI>().craftPanel.SetActive(true);
        CraftingSystem.Instance.neededs = needs;
        Transform parent = GameObject.Find("Material Panel").transform;
        foreach (var need in needs)
        {
            var mn = Instantiate(need.item, parent);
            mn.GetComponent<Image>().sprite = mn.GetComponent<MaterialNeed>().item.icon;
            mn.GetComponent<MaterialNeed>().countNeeded.text = need.count.ToString();
            mn.GetComponent<MaterialNeed>().countHadMaterial.text ="(" + mn.GetComponent<MaterialNeed>().item.Count +")";
            CraftingSystem.Instance.UpdateUI(gun);
        }
        
    }


}

