using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private bool canLoadScene;
    public bool isStartGame=false;

    void Update()
    {
        if(JoyStickCanvas.Instance)
        {
            if(JoyStickCanvas.Instance.activity && canLoadScene)
            {
                StartCoroutine(LevelManager.Instance.LevelEnd());
                JoyStickCanvas.Instance.activity = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            if(isStartGame)
            {
                StartCoroutine(LevelManager.Instance.LevelEnd());
            }else
            {
                JoyStickCanvas.Instance.ChangeShotToActivity();
                canLoadScene = true;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeActivityToShot();
            canLoadScene = false;
        }
    }
}
