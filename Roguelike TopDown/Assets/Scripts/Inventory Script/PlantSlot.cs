using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSlot : MonoBehaviour
{
    public PlantReward reward = null;
    public Image icon = null;
    public Text countText = null;

    public void Planting()
    {
        var plant=Instantiate(reward, transform.root);
        plant.GetComponent<PlantReward>();
        plant.plant.Count--;
        plant.reward.ResetRewardTime();

        var inve = GetComponentInParent<PlantController>();
        inve.HaveItem = true;
        plant.Slot = inve.slot;
        plant.SaveIDSlot(inve.namePlant,inve.slot);

        var panel = GetComponentInParent<PlantInventoryUI>();
        panel.gameObject.SetActive(false);

        PlantInventory.Instance.Remove(plant.plant);
        JoyStickCanvas.Instance.gameObject.SetActive(true);


    }
}
