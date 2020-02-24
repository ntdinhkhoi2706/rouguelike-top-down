using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantReward : MonoBehaviour
{
    public RewardController reward;
    public Plant plant = null;
    [SerializeField]
    private int plantCount=0;
    [SerializeField]
    private Sprite plant1 = null, plant2 = null;
    [SerializeField]
    private Text timeText = null;
    private SpriteRenderer sr;
    public string namePlant = null;

    private bool canTake=false;
    private int count=0;
    private int slot=0;

    public int Slot
    {
        get => PlayerPrefs.GetInt(namePlant+"slot", slot);
        set => PlayerPrefs.SetInt(namePlant+"slot", value);
    }

    public int Count
    {
        get => PlayerPrefs.GetInt(namePlant + "count", count);
        set => PlayerPrefs.SetInt(namePlant + "count", value);
    }


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SaveIDSlot(string idSlot,int slot)
    {
        PlayerPrefs.SetInt(idSlot+ namePlant, slot);
    }

    public bool CheckIDSlot(string idSlot,int slot)
    {
        if(PlayerPrefs.GetInt(idSlot+ namePlant) !=slot)
        {

            return false;
        }
        return true;
    }
    void Update()
    { 

        if (reward.CanReWard())
        {
            timeText.text = "('_')";
            sr.sprite = plant2;
        }else
        {
            sr.sprite = plant1;
            System.TimeSpan timeToReward = reward.timeToReward;
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
        
        if(JoyStickCanvas.Instance)
        {
            if(JoyStickCanvas.Instance.activity && canTake)
            {
                if(reward.CanReWard())
                {
                    Count++;
                    for (int i = 0; i < GameManager.Instance.containBuff.Count; i++)
                    {
                        if (GameManager.Instance.containBuff[i].nameBuff == plant.nameBuff)
                        {
                            UIController.Instance.buffImg.sprite = GameManager.Instance.containBuff[i].sprite;
                            UIController.Instance.buffImg.GetComponent<Animator>().SetTrigger("Do");

                            GameManager.Instance.mainBuff.Add(GameManager.Instance.containBuff[i]);
                            GameManager.Instance.containBuff.RemoveAt(i);
                        }
                    }
                    if (Count < plantCount)
                    {
                        reward.ResetRewardTime();

                    }
                    else
                    {
                        var inve = GetComponentInParent<PlantController>();
                        inve.HaveItem = false;
                        Count = 0;
                        Destroy(gameObject);
                    }
                    JoyStickCanvas.Instance.activity = false;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.tag == "Player" && reward.CanReWard())
        {
            JoyStickCanvas.Instance.ChangeShotToActivity();
            canTake = true;
        }
    }

    void OnCollisionExit2D(Collision2D target)
    {
        if (target.collider.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeActivityToShot();
            canTake = false;
        }
    }
}
