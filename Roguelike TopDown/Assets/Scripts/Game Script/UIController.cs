using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Health")]
    public Stat healthStat=null;
    [Header("Armor")]
    public Stat armorStat = null;
    [Header("Mana")]
    public Stat manaStat = null;


    [Header("UI")]
    [SerializeField]
    private GameObject deathScreen = null;
    [SerializeField]
    private Image fadeScreen = null;
    public GameObject MapDisplay = null;
    public Text walletText = null;
    public Text Level = null;
    public Text CoinText = null;

    [Header("UI Gun")]
    public Image currentGun = null;
    public Text gunText = null;

    [Header("UI Boss")]
    public Slider BossHealthSlider = null;

    [Header("Pause UI")]
    [SerializeField]
    private GameObject pauseUI = null;
    [SerializeField]
    private Image playerImg = null;

    [Header("Gun Info")]
    public GameObject gunInfo = null;
    public Text damageText = null;
    public Text critText = null;
    public Text defectionText = null;
    public Text manaText = null;

    public Image buffImg;


    
    
    private float fadeSpeed=1f;

    private bool fadeToBlack, fadeOutBlack;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        fadeOutBlack = true;
        if(walletText!=null)
        {
            walletText.text = GameManager.Instance.Wallet.ToString();
        }
    }

    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
                fadeScreen.gameObject.SetActive(false);
                
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeScreen.gameObject.SetActive(false);
                fadeToBlack = false;
            }
        }

    }
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        playerImg.sprite = PlayerController.Instance.TheBody.sprite;
        JoyStickCanvas.Instance.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        JoyStickCanvas.Instance.gameObject.SetActive(true);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Destroy(PlayerController.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
