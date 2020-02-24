using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffzone : MonoBehaviour
{
    [SerializeField]
    private GameObject debuff = null;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
            debuff1.GetComponent<Debuff>().Apply(target);
        }
        else if (target.tag == "Enemy")
        {
            var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
            debuff1.GetComponent<Debuff>().Apply(target);
        }

    }
    void OnTriggerStay2D(Collider2D target)
    {
        if(target.tag=="Player")
        {
            var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
            debuff1.GetComponent<Debuff>().Apply(target);
        }else if(target.tag =="Enemy")
        {
            var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
            debuff1.GetComponent<Debuff>().Apply(target);
        }
        
    }


}
