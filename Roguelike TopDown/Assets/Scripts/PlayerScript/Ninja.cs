using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ninja : PlayerController
{
    private bool buff=false;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns = 0.5f;

    public GameObject echo = null;
    GameObject instance;
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
        
        if (!buff)
        {
            MoveSpeed += 2f;
            
            buff = true;
        }
        if(timeBtwSpawns<=0)
        {
            instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 0.2f);
            timeBtwSpawns = startTimeBtwSpawns;
        }else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
        
        
        
    }

    public override void EndSkill()
    {
        MoveSpeed -= 2f;
        buff = false;
    }

}
