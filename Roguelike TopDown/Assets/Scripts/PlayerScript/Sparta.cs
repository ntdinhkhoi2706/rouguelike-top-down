using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparta : PlayerController
{
    [SerializeField]
    private GameObject shield=null;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Skill()
    {
        shield.SetActive(true);
   
    }
    public override void EndSkill()
    {
        shield.SetActive(false);
    }
}
