using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate: PlayerController
{
    [SerializeField]
    private GameObject turret = null;

    GameObject turretSpawn = null;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Skill()
    {
        if(turretSpawn==null)
        {
            turretSpawn= Instantiate(turret, transform.position, Quaternion.identity);
        }
    }

}
