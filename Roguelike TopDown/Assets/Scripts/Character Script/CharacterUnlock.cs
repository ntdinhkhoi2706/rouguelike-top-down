using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlock : MonoBehaviour
{
    [Header("Character UI")]
    [SerializeField]
    private GameObject unlockBtn=null;
    [SerializeField]
    private GameObject addBtn=null;
    [SerializeField]
    private GameObject upgradeBtn=null;
    [SerializeField]
    private GameObject canvas=null;
    [SerializeField]
    private Color lockedColor=Color.white;
    [SerializeField]
    private int costBuy=0;
    [SerializeField]
    private Text costBuyText=null;
    [SerializeField]
    private GameObject nameChar=null;
   

    [Header("Manager Need Activate")]
    [SerializeField]
    private GameObject UI=null;
    [SerializeField]
    private GameObject joystick=null;
    [SerializeField]
    private GameObject manager=null;
    
    
    [Header("Player")]
    [SerializeField]
    private PlayerController playerToUnlock=null;
    [SerializeField]
    private PlayerInfo playerInfo=null;


    [SerializeField]
    private Transform startTarget=null;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(PlayerPrefs.GetInt(this.name)==1)
        {
            unlockBtn.SetActive(false);
            addBtn.SetActive(true);

            sr.color  = Color.white;
        }
    }
    public void UnlockCharacter()
    {
        if(GameManager.Instance.Wallet >= costBuy)
        {
            GameManager.Instance.SpendCoin(costBuy);

            PlayerPrefs.SetInt(this.name, 1);

            unlockBtn.SetActive(false);
            addBtn.SetActive(true);
            upgradeBtn.SetActive(true);

            sr.color = Color.white;

            playerInfo.upgradeInfo.gameObject.SetActive(true);
        }
    }

    public void AddCharacter()
    {
        manager.SetActive(true);
        UI.SetActive(true);
        joystick.SetActive(true);
        
        CharacterTracker.Instance.MaxHealth = playerInfo.Health;
        CharacterTracker.Instance.CurrentHealth = playerInfo.Health;
        CharacterTracker.Instance.MaxArmor = playerInfo.Armor;
        CharacterTracker.Instance.CurrentArmor = playerInfo.Armor;
        CharacterTracker.Instance.MaxMana = playerInfo.Mana;
        CharacterTracker.Instance.CurrentMana = playerInfo.Mana;

        PlayerController player =Instantiate(playerToUnlock, transform.position, Quaternion.identity);
        player.timeCoolDown = playerInfo.TimeCoolDown;

        CharacterSelectManager.Instance.characters.Clear();
        CharacterSelectManager.Instance.characters.Add(player);

        CameraController.Instance.SetTarget(player.transform);
        CameraController.Instance.IsNotSet();

        gameObject.SetActive(false);
    }

    public void SetTarget()
    {
        if(!PlayerController.Instance)
        {
            UIController.Instance.select.SetActive(false);
            costBuyText.text = costBuy.ToString();
            CameraController.Instance.SetTarget(transform);
            CameraController.Instance.IsSet();

            canvas.SetActive(true);
            sr.color = Color.white;
            if (!unlockBtn.gameObject.activeInHierarchy)
            {
                if (playerInfo.NextUpgrade==null)
                {

                    upgradeBtn.SetActive(false);
                }
                else
                {
                    upgradeBtn.SetActive(true);
                }
            }    
        }
    }

    public void DeSelected()
    {
        UIController.Instance.select.SetActive(true);
        CameraController.Instance.SetTarget(startTarget);
        CameraController.Instance.IsNotSet();


        canvas.SetActive(false);
        if (PlayerPrefs.GetInt(this.name) != 1)
        {
            sr.color= lockedColor;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
       if(target.collider.tag == "Player")
        {
            nameChar.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D target)
    {
        if(target.collider.tag =="Player")
        {
            nameChar.SetActive(false);
        }
    }
}
