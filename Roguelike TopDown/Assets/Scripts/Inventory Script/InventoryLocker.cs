using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLocker : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory=null;

    private bool canOpen;
    void Update()
    {
        if(JoyStickCanvas.Instance)
        {
            if(JoyStickCanvas.Instance.activity && canOpen)
            {
                JoyStickCanvas.Instance.gameObject.SetActive(false);
                inventory.SetActive(true);
                canOpen = false;
                JoyStickCanvas.Instance.activity = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.collider.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeShotToActivity();
            canOpen = true;
        }
    }

    void OnCollisionExit2D(Collision2D target)
    {
        if(target.collider.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeActivityToShot();
            canOpen = false;
        }
    }
}
