using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private Text enemyText = null, coinText = null,walletText=null;

    [SerializeField]
    private Image player = null;
    private int money;

    void Start()
    {
        player.sprite = PlayerController.Instance.TheBody.sprite;
        enemyText.text = GameManager.Instance.enemyCount.ToString();
        coinText.text = LevelManager.Instance.CurrentCoins.ToString();
        money = GameManager.Instance.enemyCount * 2 + LevelManager.Instance.CurrentCoins / 2;
        walletText.text = money.ToString();

    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("CharacterSelect");
        
        GameManager.Instance.enemyCount = 0;
        GameManager.Instance.Wallet += money;
        Destroy(PlayerController.Instance.gameObject);
        Destroy(ObjectPooling.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
    }
}
