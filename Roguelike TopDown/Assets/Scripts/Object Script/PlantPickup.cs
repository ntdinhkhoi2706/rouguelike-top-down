﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPickup : MonoBehaviour
{
    public Plant item = null;
    private bool canPickUp;


    void Update()
    {
        if (JoyStickCanvas.Instance)
        {
            if (JoyStickCanvas.Instance.activity && canPickUp)
            {
                bool wasPickedUp = PlantInventory.Instance.Add(item);

                if (wasPickedUp)
                {
                    JoyStickCanvas.Instance.activity = false;
                    Destroy(gameObject);
                }


            }

        }



    }
    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeShotToActivity();
            canPickUp = true;

        }


    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            canPickUp = false;
            JoyStickCanvas.Instance.ChangeActivityToShot();
        }
    }
}
