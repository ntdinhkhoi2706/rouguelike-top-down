using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zedla : PlayerController
{
    [SerializeField]
    private GameObject healthZone=null;
    private GameObject healthZoneSpawn=null;
    private bool spawn = false;
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
        if(!spawn)
        {
            healthZoneSpawn = ObjectPooling.Instance.GetGameObject(healthZone, transform.position, Quaternion.identity);
            spawn = true;
        }
    }

    public override void EndSkill()
    {
        healthZoneSpawn.SetActive(false);
        spawn = false;
    }
}
