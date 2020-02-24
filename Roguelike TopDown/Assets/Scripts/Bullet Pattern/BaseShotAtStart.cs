using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShotAtStart : MonoBehaviour
{
    [SerializeField]
    private BaseShot shot=null;


    private bool canShot;

    void OnEnable()
    {
        if(canShot)
        {
            shot.Shot();
            canShot = false;
        }
        Invoke("Deactivate", 1f);
    }
    void OnDisable()
    {
        canShot = true;
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
   


}
