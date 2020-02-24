using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Action")]
    [SerializeField]
    private EnemyAction[] actions=null;

    [Header("Rotate")]
    [SerializeField]
    private Transform gunHand=null;


    [Header("Fx")]
    [SerializeField]
    private float health=0;
    [SerializeField]
    private GameObject[] deathSlater=null;
    [SerializeField]
    private GameObject enemyHitFx=null;
    [SerializeField]
    private GameObject[] collect=null;
    public SpriteRenderer theBody=null;
    [SerializeField]
    private GameObject target=null;
    
    [Header("Enemy Element")]
    public GameObject ice=null, poison=null, fire=null;

    private float wanderCounter, pauseCounter;


    private Vector2 moveDirection;
    private Vector2 wanderDirection;

    private float actionCounter;
    private int currentAction;

    private float shotCounter;

    public bool isAlive { get; set; }
    public bool canMove { get; set; }
    

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        actionCounter = actions[currentAction].actionLength;

        isAlive = true;
        pauseCounter = Random.Range(actions[currentAction].pauseLength * 0.75f, actions[currentAction].pauseLength * 1.25f);

    }

    void CanActivate()
    {
        canMove = true;
    }

    void Update()
    {
        
        if(isAlive && canMove)
        {

                Rotate();
                if (gameObject == PlayerController.Instance.Enemy)
                {
                    target.SetActive(true);
                }
                else
                {
                    target.SetActive(false);
                }

                if (actionCounter > 0)
                {
                    actionCounter -= Time.deltaTime;

                    EnemyMovement();
                    EnemyAttack();

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
            
            
        }




    }

    void EnemyMovement()
    {
        if (actions[currentAction].shouldMove)
        {
            if (actions[currentAction].shouldWander)
            {
                if(wanderCounter>0)
                {
                    wanderCounter -= Time.deltaTime;

                    moveDirection = wanderDirection;
                    

                    if (wanderCounter<=0)
                    {
                        pauseCounter = Random.Range(actions[currentAction].pauseLength * 0.75f, actions[currentAction].pauseLength * 1.25f);
                    }
                }

                if(pauseCounter >0)
                {
                    pauseCounter -= Time.deltaTime;
                    rb.velocity = Vector2.zero;
                    if(pauseCounter <=0)
                    {
                        wanderCounter = Random.Range(actions[currentAction].wanerLength * 0.75f, actions[currentAction].wanerLength * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f),0);
   
                    }
                }
                
                
            }

            if(actions[currentAction].shouldRunAway && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < actions[currentAction].runAwayRange)
            {
                moveDirection = transform.position - PlayerController.Instance.transform.position;
                
            }

        }
        moveDirection.Normalize();
        rb.velocity = moveDirection * actions[currentAction].moveSpeed;
       


        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    void EnemyAttack()
    {
        if (actions[currentAction].shouldShoot)
        {
            if(shotCounter>=0)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    actions[currentAction].shot.Shot();
                    shotCounter = actions[currentAction].timeBetweenShot;
                }
            }
            
        }
    }

    public float Rotate()
    {
        Vector3 targ = PlayerController.Instance.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;

        if (targ.x < objectPos.x)
        {
            transform.localScale = Vector3.one;
            gunHand.localScale = new Vector3(-1f, -1f, 1f);
            


        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunHand.localScale = Vector3.one;
        }
        Vector2 offset = new Vector2(targ.x - objectPos.x, targ.y - objectPos.y);
        float angle1 = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunHand.rotation = Quaternion.Euler(new Vector3(0, 0, angle1));

        return angle1;
    }




    public void DealDamage(float damage)
    {
        AudioManager.Instance.PlaySFX(2);
        health -= damage;
        Instantiate(enemyHitFx, transform.position, Quaternion.identity);

        if(health <=0)
        {
            AudioManager.Instance.PlaySFX(1);
            if (GameManager.Instance.mainBuff.Count>0)
            {
                foreach(var buff in GameManager.Instance.mainBuff)
                {
                    if(buff.nameBuff.Contains("ChanceHealthAfterKill"))
                    {
                        int rdHealth = Random.Range(0, 100);
                        if (rdHealth <= 10)
                        {
                            PlayerHealthController.Instance.HealthPlayer(1);
                        }
                    }
                    
                }
            }
            isAlive = false;
            GameManager.Instance.enemyCount++;
            gameObject.SetActive(false);
            int rd = Random.Range(0, deathSlater.Length);

            int rotate = Random.Range(0, 4);

            Instantiate(deathSlater[rd], transform.position, Quaternion.Euler(0, 0, rotate * 90f));

            int rdCoin = Random.Range(0, 100);
            int rdamount = Random.Range(1, 5);

            if(10 > rdCoin)
            {
                var spawnPlant = false;
                int rdPlant = Random.Range(0,PlantInventory.Instance.itemsPickup.Count);
                for (int i=0;i<PlantInventory.Instance.itemsPickup.Count;i++)
                {
                    if(i==rdPlant)
                    {
                        if (!spawnPlant)
                        {
                            Vector2 tmp = transform.position;
                            tmp.x += Random.Range(-2f, 2f);
                            if(tmp.x < PlayerController.Instance.transform.position.x)
                            {
                                tmp.x += 1f;
                            }else
                            {
                                tmp.x -= 1f;
                            }
                            tmp.y += Random.Range(-2f, 2f);
                            if(tmp.y < PlayerController.Instance.transform.position.y)
                            {
                                tmp.y += 1f;
                            }else
                            {
                                tmp.y -= 1f;
                            }
                            Instantiate(PlantInventory.Instance.itemsPickup[i], tmp, Quaternion.identity);
                            spawnPlant = true;
                        }
                    }
                    

                }
            }else if(15 > rdCoin)
            {
                var spawnItem = false;
                int rdItem = Random.Range(0, Inventory.Instance.itemsPickup.Count);
                for (int i = 0; i < PlantInventory.Instance.itemsPickup.Count; i++)
                {
                    if (i == rdItem)
                    {
                        if (!spawnItem)
                        {
                            Vector2 tmp = transform.position;
                            tmp.x += Random.Range(-2f, 2f);
                            if (tmp.x < PlayerController.Instance.transform.position.x)
                            {
                                tmp.x += 1f;
                            }
                            else
                            {
                                tmp.x -= 1f;
                            }
                            tmp.y += Random.Range(-2f, 2f);
                            if (tmp.y < PlayerController.Instance.transform.position.y)
                            {
                                tmp.y += 1f;
                            }
                            else
                            {
                                tmp.y -= 1f;
                            }
                            Instantiate(Inventory.Instance.itemsPickup[i], tmp, Quaternion.identity);
                            spawnItem = true;
                        }
                    }


                }
            }
            else if (85 > rdCoin)
            {
                for (int i = 0; i < rdamount; i++)
                {
                    int rdItems = Random.Range(0, collect.Length);
                    Vector3 tmp = transform.position;
                    tmp.x += Random.Range(0, 3);
                    tmp.y += Random.Range(0, 3);
                    Instantiate(collect[rdItems], tmp, Quaternion.identity);
                }
            }
        }
    }


    
 



}

[System.Serializable]
public class EnemyAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public float moveSpeed;

    public bool shouldRunAway;
    public float runAwayRange;

    public bool shouldWander;
    public float wanerLength;
    public float pauseLength;

    public bool shouldShoot;
    public BaseShot shot;
    public float timeBetweenShot;
}
