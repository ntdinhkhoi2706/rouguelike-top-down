using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : PlayerController
{
    private bool buff;
    private float defaultSpeed;
    public override void Start()
    {
        defaultSpeed = MoveSpeed;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Skill()
    {
        rb.AddForce(gunArm.transform.right * 120f);
        if(!buff)
        {
            MoveSpeed = 0;
            anim.SetTrigger("Dash");
            buff = true;
            PlayerHealthController.Instance.MakeInvincible(skillDuration);
            
        }
    }

    public override void EndSkill()
    {
        buff = false;
        MoveSpeed = defaultSpeed;
    }


}
