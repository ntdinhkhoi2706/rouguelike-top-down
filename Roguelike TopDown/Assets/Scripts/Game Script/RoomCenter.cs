using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared=true;
    public bool canInitialize=false;

    [SerializeField]
    private GameObject[] enemyPrefabs=null;
    [SerializeField]
    private int number=0;

    public Room theRoom { get; set; }


    

    

    private List<GameObject> enemy = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            theRoom.closedWhenEntered = true;
            
        }
        
    }

    void Update()
    {
        if(theRoom.playerIn)
        {
            if (enemy.Count >= 0 && theRoom.playerIn && openWhenEnemiesCleared)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (!enemy[i].gameObject.activeInHierarchy)
                    {
                        enemy.RemoveAt(i);
                        i--;
                    }
                }

            }
            if (enemy.Count == 0)
            {
                theRoom.OpenDoors();
                CameraController.Instance.SetTargetRoom(null);
                canInitialize = false;
            }
            if (!canInitialize)
            {
                if (enemy.Count == 0)
                {
                    theRoom.OpenDoors();
                    CameraController.Instance.SetTargetRoom(null);
                }
            }
        }
        
       
        
        
    }

    void Instantitae()
    {
        for(int i=0;i<number;i++)
        {
            Vector3 rdTmp = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            GameObject tmp = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],rdTmp,Quaternion.identity);
            enemy.Add(tmp);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            if (canInitialize)
            {
                CameraController.Instance.SetTargetRoom(transform);
                Instantitae();
            }
        }
    }


    

    
    
   

    
}
