using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("Movement")]
    public float MoveSpeed=5;
    public Joystick joystick { get; set; }


    [Header("Shoot")]
    public Transform gunArm=null;

    public WeaponManager weaponManager { get; protected set; }

    [Header("Fx")]
    [SerializeField]
    private SpriteRenderer theBody = null;
    

    [Header("Skill")]
    public float skillDuration;
    public float timeCoolDown { get; set; }
    public bool canUseSkill { get; set; }

    private float skillDurationCounter;
    private float timeCoolDownCounter;
    private bool isCoolDown;
    

    public GameObject debuffPoison=null,debuffFire = null, debuffIce = null;

    public bool CanMove { get; set; }
    public bool CanShoot { get; set; }
    public SpriteRenderer TheBody { get => theBody; set => theBody = value; }
    public GameObject Enemy { get; set; }
    public bool UseSkill { get; set; }
    [HideInInspector]
    public Rigidbody2D rb;
    protected Animator anim;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        
    }
    // Start is called before the first frame update
    public virtual void Start()
    { 
        skillDurationCounter = skillDuration;
        canUseSkill = true;
        weaponManager = GetComponent<WeaponManager>();
        CanMove = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
            joystick = GameObject.Find("Variable Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(CanMove && CharacterSelectManager.Instance.characters.Contains(this))
        {
            UsingSkill();
            PlayerMovement();
            RotateGun();

            if(CanShoot)
            {
                weaponManager.Attack();
                weaponManager.Hold();
            }else if(!CanShoot)
            {
                weaponManager.DropToAttack();
                if (weaponManager.current_Weapon.defaultConfig.typeAttack==TypeAttack.Hold)
                {
                    weaponManager.Attack();
                }
                
            }
        }else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Walk", false);
        }
        
    }

   
    void UsingSkill()
    {
        if (UseSkill && canUseSkill)
        {
            Skill();
            if (skillDurationCounter > 0)
            {
                skillDurationCounter -= Time.deltaTime;
                JoyStickCanvas.Instance.skillUI.CurrentValue = skillDurationCounter;
                if (skillDurationCounter < 0)
                {
                    JoyStickCanvas.Instance.skillUI.Initilize(0, timeCoolDown);
                    EndSkill();
                    skillDurationCounter = skillDuration;
                    UseSkill = false;
                    isCoolDown = true;
                    canUseSkill = false;
                }
            }
        }

        if (!UseSkill && isCoolDown)
        {

            if (timeCoolDownCounter <= timeCoolDown)
            {

                timeCoolDownCounter += Time.deltaTime;


                JoyStickCanvas.Instance.skillUI.CurrentValue = timeCoolDownCounter;

            }

            if (timeCoolDownCounter > timeCoolDown)
            {
                JoyStickCanvas.Instance.skillUI.Initilize(skillDuration, skillDuration);
                isCoolDown = false;
                canUseSkill = true;
                timeCoolDownCounter = 0;
            }
        }
    }
    void PlayerMovement()
    {
        rb.velocity = new Vector2(joystick.Horizontal * MoveSpeed, joystick.Vertical * MoveSpeed);

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }


    }

    public float RotateGun()
    {
        Enemy = GamePlayController.Instance.FindClosestEnemy();
        float angle;
        Vector2 offset;
        Vector3 objectPos;


        if (Enemy && Enemy.GetComponentInChildren<SpriteRenderer>().isVisible)
        {
            Vector3 targ = Enemy.transform.position;
            targ.z = 0f;

            objectPos = transform.position;

            if (targ.x <= objectPos.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }
            offset = new Vector2(targ.x - objectPos.x, targ.y - objectPos.y);
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            return angle;


        }
        else
        {
            if (joystick.Horizontal >= 0.2f)
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }
            else if (joystick.Horizontal <= -0.2f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            offset = new Vector2(joystick.Horizontal, joystick.Vertical);
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            return angle;
        }
          
    }
    public virtual void Skill()
    {

    }

    public virtual void EndSkill()
    {

    }
    public void PlayerUseSkill()
    {
            UseSkill = true;
    }

    public void CheckCanShot()
    {
        CanShoot = true;
    }
    public void DelCanShot()
    {
        CanShoot = false;
    }

    
}

    










