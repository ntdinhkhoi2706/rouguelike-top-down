using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanDomBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bullet=null;
    void Start()
    {
        int rd = Random.Range(0, bullet.Length);
        Instantiate(bullet[rd],transform);
    }

}
