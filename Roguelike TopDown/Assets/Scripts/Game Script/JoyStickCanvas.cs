using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickCanvas : MonoBehaviour
{
    public static JoyStickCanvas Instance;

    [SerializeField]
    private GameObject shotBtn=null, activityBtn=null;
    [SerializeField]
    private Button skillBtn=null;

    public bool activity { get; set; }

    public Stat skillUI=null;
    void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        skillUI.Initilize(PlayerController.Instance.skillDuration, PlayerController.Instance.skillDuration);
    }

    void Update()
    {
        if(PlayerController.Instance.UseSkill)
        {      
            skillBtn.interactable = false;
        }else if(PlayerController.Instance.canUseSkill)
        {
            skillBtn.interactable = true;
        }
    }

    public void SwitchWeapon()
    {
        WeaponManager.Instance.SwitchWeapon();
    }
    public void Shooting()
    {
        PlayerController.Instance.CheckCanShot();
    }

    public void DeShooting()
    {
        PlayerController.Instance.DelCanShot();
    }

    public void UsingSkill()
    {
        PlayerController.Instance.PlayerUseSkill();
    }

    public void Activity()
    {
        activity = true;
    }

    public void ChangeShotToActivity()
    {
        shotBtn.SetActive(false);
        activityBtn.SetActive(true);
    }

    public void ChangeActivityToShot()
    {
        shotBtn.SetActive(true);
        activityBtn.SetActive(false);
    }
}
