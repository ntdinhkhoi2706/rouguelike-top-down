using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField]
    private Transform target=null;
    [SerializeField]
    private Transform targetRoom=null;


    private Camera mainCamera;
    private bool isSet;

    float defaultHeight;
    float defaultWidth;

    
    
    Vector3 cameraPos;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;
        cameraPos = mainCamera.transform.position;

        defaultHeight = mainCamera.orthographicSize;
        defaultWidth = mainCamera.orthographicSize * mainCamera.aspect;
 
    }


    // Update is called once per frame
    void Update()
    {
        if (isSet)
        {
            mainCamera.orthographicSize = 3f;
            mainCamera.transform.position = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
        }
        else
        {
            mainCamera.orthographicSize = defaultWidth / mainCamera.aspect;
            Vector3 newPos = new Vector3(cameraPos.x, -1 * (defaultHeight - mainCamera.orthographicSize), cameraPos.z);
            if (target)
            {

                if (targetRoom)
                {
                    if (mainCamera.transform.position.x != targetRoom.position.x || mainCamera.transform.position.y != targetRoom.position.y)
                    {
                        mainCamera.transform.position = new Vector3(targetRoom.position.x, targetRoom.position.y, newPos.z);
                    }
                }
                else
                {
                    mainCamera.transform.position = new Vector3(target.position.x + newPos.x, target.position.y + newPos.y, cameraPos.z);
                }
            }
            else
            {
                mainCamera.transform.position = newPos;
            }

        }
       
    }

    public void IsSet()
    {
        isSet = true;
    }
    public void IsNotSet()
    {
        isSet = false;
    }
    public void SetTarget(Transform newTarget)
    {
        this.target = newTarget;
    }

    public void SetTargetRoom(Transform newTarget)
    {
        this.targetRoom = newTarget;
    }


}
