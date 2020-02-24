using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private float waitToLoad=2.5f;

    [SerializeField]
    private string level=null,nextLevel=null;
    [SerializeField]
    private bool isStartGame=false, isFinal = false;

    public int levelIndex=0;


    [SerializeField]
    private Transform startPoint=null;
    

    public int CurrentCoins { get ; set ; }
    [SerializeField]
    private Joystick joystick=null;

    void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        if (PlayerController.Instance && startPoint != null)
        {
            PlayerController.Instance.transform.position = Vector2.MoveTowards(PlayerController.Instance.transform.position, startPoint.position, 1000);
            PlayerController.Instance.CanMove = true;
        }
        if (UIController.Instance.Level != null)
        {
            UIController.Instance.Level.text = levelIndex + "-" + GameManager.Instance.levelIndex;
        }

        PlayerController.Instance.joystick = joystick;
        CurrentCoins = CharacterTracker.Instance.Coin;
        UIController.Instance.CoinText.text = CurrentCoins.ToString();

        if(WeaponManager.Instance.current_Weapon)
        {
            UIController.Instance.currentGun.sprite = WeaponManager.Instance.current_Weapon.gunUI;
            UIController.Instance.gunText.text = WeaponManager.Instance.current_Weapon.gunName;
        }



    }



    public IEnumerator LevelEnd()
    {
   
        PlayerController.Instance.CanMove = false;

        UIController.Instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.Instance.Coin = CurrentCoins;

        CharacterTracker.Instance.MaxHealth = PlayerHealthController.Instance.MaxHealth;
        CharacterTracker.Instance.CurrentHealth = PlayerHealthController.Instance.CurrentHealth;

        CharacterTracker.Instance.MaxArmor = PlayerHealthController.Instance.MaxArmor;
        CharacterTracker.Instance.CurrentArmor = PlayerHealthController.Instance.CurrentArmor;

        CharacterTracker.Instance.MaxMana = PlayerHealthController.Instance.MaxMana;
        CharacterTracker.Instance.CurrentMana = PlayerHealthController.Instance.CurrentMana;
        if(nextLevel != null && level != null)
        {
            AudioManager.Instance.PlaySFX(12);
            if (isStartGame)
            {
                SceneManager.LoadScene(nextLevel);
            }else if(isFinal)
            {
                SceneManager.LoadScene(nextLevel);
                AudioManager.Instance.PlayLevelWin();
            }else if (GameManager.Instance.levelIndex == 5)
            {
                SceneManager.LoadScene(nextLevel);
                GameManager.Instance.levelIndex = 1;
            }
            else
            {
                SceneManager.LoadScene(level);
                GameManager.Instance.levelIndex++;
            }
        }
        

    }

    public void GetCoins(int amount)
    {
        CurrentCoins += amount;
        UIController.Instance.CoinText.text = CurrentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        CurrentCoins -= amount;

        if(CurrentCoins <0)
        {
            CurrentCoins = 0;
        }
        UIController.Instance.CoinText.text = CurrentCoins.ToString();
    }
}
