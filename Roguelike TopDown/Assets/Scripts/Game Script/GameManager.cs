using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private int wallet=0;

    public int enemyCount=0;
    public int levelIndex=1;

    public int Wallet
    {
        get => PlayerPrefs.GetInt("Wallet", wallet);
        set => PlayerPrefs.SetInt("Wallet", value);
    }


    public List<MainBuff> containBuff = new List<MainBuff>();
    public List<MainBuff> mainBuff = new List<MainBuff>();
    void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SpendCoin(int amount)
    {
        Wallet -= amount;
        UIController.Instance.walletText.text = Wallet.ToString();
    }
   
}

[System.Serializable]
public class MainBuff
{
    public string nameBuff;
    public string infoBuff;
    public Sprite sprite;
}
