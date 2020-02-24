using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{

    [SerializeField]
    private int hoursToReward=10;
    [SerializeField]
    private int minutesToReward=0;
    [SerializeField]
    private int secondToReward=0;

    




    public System.DateTime nextRewardTime => GetNextRewardTime();
    public System.TimeSpan timeToReward => nextRewardTime.Subtract(System.DateTime.Now);

    public bool CanReWard()
    {
        return nextRewardTime <= System.DateTime.Now;
    }



    public void ResetRewardTime()
    {
        System.DateTime nextReward = System.DateTime.Now.Add(new System.TimeSpan(hoursToReward, minutesToReward, secondToReward));
        SaveNextRewardTime(nextReward);
    }

    public void SaveNextRewardTime(System.DateTime time)
    {
        PlayerPrefs.SetString(transform.root.name, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private System.DateTime GetNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(transform.root.name, string.Empty);

        if (!string.IsNullOrEmpty(nextReward))
        {
            return System.DateTime.FromBinary(System.Convert.ToInt64(nextReward));
        }
        else
        {
            return System.DateTime.Now;
        }
    }
}
