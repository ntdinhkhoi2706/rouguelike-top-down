using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    

    [SerializeField]
    private GameObject[] doors=null;
    [SerializeField]
    private GameObject mapHider=null;

    public bool closedWhenEntered { get; set; }
    public bool playerIn { get; set; }

    public void OpenDoors()
    {
       
       foreach (GameObject door in doors)
       {
            door.SetActive(false);
            closedWhenEntered = false;
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            
            playerIn = true;
            CameraController.Instance.SetTarget(PlayerController.Instance.transform);

            if (closedWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            mapHider.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag=="Player")
        {
            playerIn = false;
            
        }
    }
}
