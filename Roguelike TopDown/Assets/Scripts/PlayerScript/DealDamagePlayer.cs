using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamagePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            PlayerHealthController.Instance.DamagePlayer();
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            PlayerHealthController.Instance.DamagePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.tag == "Player")
        {
            PlayerHealthController.Instance.DamagePlayer();
        }
    }

    void OnCollisionStay2D(Collision2D target)
    {
        if (target.collider.tag == "Player")
        {
            PlayerHealthController.Instance.DamagePlayer();
        }
    }
}
