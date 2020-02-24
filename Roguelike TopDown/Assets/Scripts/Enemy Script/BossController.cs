using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Sequences")]
    [SerializeField]
    private BossSwequence[] sequences=null;
    [SerializeField]
    private float health=10000;

    [Header("Boss Element")]
    [SerializeField]
    private GameObject levelExit=null;
    [SerializeField]
    private Transform gunHand=null;
    public GameObject fire=null, ice=null, poison=null;

    private BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float wanderCounter, pauseCounter;
    private int currentSequence;
    private float shotCounter;
    private Vector2 moveDirection;
    private Vector2 wanderDirection;

    public bool isAlive { get; set; }
    public bool canMove { get; set; }
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isAlive = true;
        canMove = true;
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;

        pauseCounter = Random.Range(actions[currentAction].pauseLength * 0.75f, actions[currentAction].pauseLength * 1.25f);

        UIController.Instance.BossHealthSlider.maxValue = health;
        UIController.Instance.BossHealthSlider.value = health;
    }

    void Update()
    {
        if(isAlive&&canMove)
        {
            if (actionCounter > 0)
            {
                actionCounter -= Time.deltaTime;

                moveDirection = Vector2.zero;

                BossMovement();

                BossAttack();

            }
            else
            {
                currentAction++;
                if (currentAction >= actions.Length)
                {
                    currentAction = 0;
                }
                actionCounter = actions[currentAction].actionLength;
            }
            Rotate();
        }
        

    }
    public float Rotate()
    {
        Vector3 targ = PlayerController.Instance.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;

        if (targ.x <= objectPos.x)
        {
            gunHand.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            gunHand.localScale = Vector3.one;
        }
        Vector2 offset = new Vector2(targ.x - objectPos.x, targ.y - objectPos.y);
        float angle1 = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunHand.rotation = Quaternion.Euler(new Vector3(0, 0, angle1));

        return angle1;
    }

    void BossMovement()
    {
        if (actions[currentAction].shouldMove)
        {
            if (actions[currentAction].shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;

                    moveDirection = wanderDirection;


                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(actions[currentAction].pauseLength * 0.75f, actions[currentAction].pauseLength * 1.25f);
                    }
                }

                if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;
                    rb.velocity = Vector2.zero;
                    if (pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(actions[currentAction].wanderLength * 0.75f, actions[currentAction].wanderLength * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));


                    }
                }


            }
            if (actions[currentAction].shouldChasePlayer)
            {
                moveDirection = PlayerController.Instance.transform.position - transform.position;
                moveDirection.Normalize();
            }

            if (actions[currentAction].moveToPoints && Vector3.Distance(transform.position, actions[currentAction].pointToMoveTo.position) > 0.5f)
            {
                moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                moveDirection.Normalize();
            }
        }
       
        rb.velocity = moveDirection * actions[currentAction].moveSpeed;
    }

    void BossAttack()
    {
        if (actions[currentAction].shouldShoot)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                foreach(var s in actions[currentAction].shot)
                {
                    if(s!=null)
                    {
                        s.Shot();
                    }
                }
                
                shotCounter = actions[currentAction].timeBetweenShots;

                
            }
        }
    }

    public void BossTakeDamage(float amount)
    {
        health -= amount;

        if(health <=0)
        {
            var spawnPlant = false;
            
            foreach (var plant in PlantInventory.Instance.itemsPickup)
            {
                if(!spawnPlant)
                {
                    Vector2 tmp = transform.position;
                    tmp.x += Random.Range(-2f, 2f);
                    tmp.y += Random.Range(-2f, 2f);
                    Instantiate(plant, tmp, Quaternion.identity);
                    spawnPlant = true;
                }
                
            }
            var spawnItem = false;

            foreach (var item in Inventory.Instance.itemsPickup)
            {
                if (!spawnItem)
                {
                    Vector2 tmp = transform.position;
                    tmp.x += Random.Range(-2f, 2f);
                    tmp.y += Random.Range(-2f, 2f);
                    Instantiate(item, tmp, Quaternion.identity);
                    spawnItem = true;
                }

            }
            isAlive = false;
            levelExit.SetActive(true);
            gameObject.SetActive(false);
            
        }
        else
        {
            if(health <=sequences[currentSequence].endSequenceHealth &&  currentSequence < sequences.Length-1)
            {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
        UIController.Instance.BossHealthSlider.value = health;
    }

}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldWander;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public float pauseLength;
    public float wanderLength;
    public bool moveToPoints;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public float timeBetweenShots;
    public BaseShot[] shot;
}

[System.Serializable]
public class BossSwequence
{
    [Header("Sequence")]
    public BossAction[] actions;

    public int endSequenceHealth;
}
