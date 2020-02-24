using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private int healthPlus=1;
    private float waitToBeCollected = 0.5f;

    void Update()
    {
        if(waitToBeCollected >0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player" && waitToBeCollected <=0)
        {
            AudioManager.Instance.PlaySFX(6);
            PlayerHealthController.Instance.HealthPlayer(healthPlus);
            Destroy(gameObject);
        }
    }
}
