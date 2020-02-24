using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject layoutRoom=null;
    [SerializeField]
    private int numberOfRooms=5;
    [SerializeField]
    private Color startColor=Color.blue, endColor=Color.red,shopColor=Color.yellow;
    [SerializeField]
    private Transform generatorPoint=null;
    [SerializeField]
    private LayerMask whatIsRoom=1;
    [SerializeField]
    private bool includeShop=false;
    [SerializeField]
    private int minDistanceToShop=1, maxDistanceToShop=2;

    [SerializeField]
    private RoomPrefabs rooms=null;

    public RoomCenter[] centerShop=null;
    public RoomCenter centerStart=null, centerEnd=null,centerEndBuff=null;
    public RoomCenter[] potentialCenters=null; 

    public enum Direction
    {
        up,right,down,left
    }

    private Direction selectedDirection;

    private float xOffSet = 26f, yOffSet = 16f;

    private GameObject endRoom,shopRoom;

    private List<GameObject> layoutRoomObjs = new List<GameObject>();
    private List<GameObject> generatedOutlines = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitializeRooms();
        CreateShopRoom();
        RoomOutLine();
        CreateOutlineTileMap();
        
        
    }

    void InitializeRooms()
    {
        Instantiate(layoutRoom, generatorPoint.position, Quaternion.identity).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGeneratorPoint();

        for (int i = 0; i < numberOfRooms; i++)
        {
            GameObject newRoom= Instantiate(layoutRoom, generatorPoint.position, Quaternion.identity);
            newRoom.transform.parent = transform;

            layoutRoomObjs.Add(newRoom);

            if(i+1 == numberOfRooms)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjs.RemoveAt(layoutRoomObjs.Count - 1);

                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGeneratorPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            {
                MoveGeneratorPoint();
            }
        }
    }

    void RoomOutLine()
    {
        CreateRoomOutLine(Vector3.zero);
        foreach(GameObject room in layoutRoomObjs)
        {
            CreateRoomOutLine(room.transform.position);
        }
        CreateRoomOutLine(endRoom.transform.position);

        if(includeShop)
        {
            CreateRoomOutLine(shopRoom.transform.position);
        }
    }

    void CreateOutlineTileMap()
    {
        foreach (GameObject outline in generatedOutlines)
        {
            bool generateCenter = true;
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, Quaternion.identity).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }
            if (outline.transform.position == endRoom.transform.position)
            {
                if(GameManager.Instance.levelIndex==1 || GameManager.Instance.levelIndex == 3)
                {
                    Instantiate(centerEndBuff, outline.transform.position, Quaternion.identity).theRoom = outline.GetComponent<Room>();
                }else
                {
                    Instantiate(centerEnd, outline.transform.position, Quaternion.identity).theRoom = outline.GetComponent<Room>();
                }
                
                generateCenter = false;
            }

            if(includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    int rd = Random.Range(0, centerShop.Length);
                    Instantiate(centerShop[rd], outline.transform.position, Quaternion.identity).theRoom = outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }

            if (generateCenter)
            {
                int centerSelect = Random.Range(0, potentialCenters.Length);
                Instantiate(potentialCenters[centerSelect], outline.transform.position, Quaternion.identity).theRoom = outline.GetComponent<Room>();
            }



        }
    }

  

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/
    }

     void MoveGeneratorPoint()
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0, yOffSet, 0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0, -yOffSet, 0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffSet, 0, 0);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffSet, 0, 0);
                break;
        }
    }

    void CreateShopRoom()
    {
        if(includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop+1);
            shopRoom = layoutRoomObjs[shopSelector];
            layoutRoomObjs.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }


    }

     void CreateRoomOutLine(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f),0.2f,whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), 0.2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffSet, 0, 0f), 0.2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffSet, 0, 0f), 0.2f, whatIsRoom);

        int directionCount = 0;
        if (roomAbove)
            directionCount++;
        if (roomBelow)
            directionCount++;
        if (roomRight)
            directionCount++;
        if (roomLeft)
            directionCount++;

        switch(directionCount)
        {
            case 0:
                Debug.LogError("Found no room exist!");
                break;
            case 1:
                if(roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, Quaternion.identity));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, Quaternion.identity));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, Quaternion.identity));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, Quaternion.identity));
                }
                break;
            case 2:
                if (roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, Quaternion.identity));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, Quaternion.identity));
                }
                if (roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, Quaternion.identity));
                }
                if (roomRight && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, Quaternion.identity));
                }
                if(roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpLeft, roomPosition, Quaternion.identity));
                }
                if(roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownRight, roomPosition, Quaternion.identity));
                }
                break;
            case 3:
                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftRight, roomPosition, Quaternion.identity));
                }
                if (roomAbove && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpLeftDown, roomPosition, Quaternion.identity));
                }
                if (roomRight && roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpLeftRight, roomPosition, Quaternion.identity));
                }
                if (roomRight && roomBelow && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, Quaternion.identity));
                }
                break;
            case 4:
                if(roomAbove && roomBelow && roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.fourWay, roomPosition, Quaternion.identity));
                }
                break;

        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown,doubleLeftRight,doubleUpRight,doubleUpLeft,doubleDownRight,doubleDownLeft,
        tripleUpRightDown,tripleUpLeftDown,tripleUpLeftRight,tripleDownLeftRight,
        fourWay;
}
