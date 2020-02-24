using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField]
    private int coinValue=0;


    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 1000)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, 10 * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            AudioManager.Instance.PlaySFX(4);
            LevelManager.Instance.GetCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
