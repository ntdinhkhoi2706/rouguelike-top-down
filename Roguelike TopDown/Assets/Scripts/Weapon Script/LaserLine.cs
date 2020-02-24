using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour
{
    public Transform rayBeginPos = null;

    public LayerMask enemyHitLayer=1;

    [Range(5, 50)]
    public float maxLaserSize=10;

    [Range(1, 20)]
    public float laserGlowMultiplier=10;

    [SerializeField]
    private GameObject debuff = null;


    public GameObject laserHitEmitter = null;

    public GameObject laserHitEnd = null;

    public GameObject laserMelEmitter = null;

    public float rayDuration=0.5f;

    public Transform laserGlow = null;

    public bool laserOn = false;

    private LineRenderer lineRenderer;
    public Animator anim = null;

    private float length = 0;
    float lerpTime = 1f;

    private float currentLerpTime = 0;

    GameObject[] hitParticle;
    GameObject meltParticle = null;

    private float lastShot;
    private float time=0.5f;

    private EnemyController enemy;
    private Laser laser;

    [HideInInspector]
    public bool canFire = false;

    void Start()
    {
        laserGlow.gameObject.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.sortingOrder = 5;

        anim = GetComponent<Animator>();

        hitParticle = new GameObject[100];


        laser = GetComponentInParent<Laser>();
    }

    void ActivateLaser()
    {
        laserOn = true;
        //Invoke("DeactivateLaser", rayDuration);
    }

    void DeactivateLaser()
    {
        anim.SetBool("StartLaser", false);
        laserOn = false;
    }

    void Update()
    {
        FireLaser();

        if(laser.typeAttack == Laser.TypeAttack.Hold)
        {
            if (!PlayerController.Instance.CanShoot)
            {
                laserOn = false;
                anim.SetBool("StartLaser", false);
                canFire = false;
            }
        }
        
        if (laserOn)
        { 
            Vector2 laserDir = transform.right;

            lineRenderer.enabled = true;
            RaycastHit2D[] hit = Physics2D.RaycastAll(rayBeginPos.position, laserDir, 300, enemyHitLayer);

            if (meltParticle == null)
            {
                meltParticle = Instantiate(laserMelEmitter, rayBeginPos.position, Quaternion.identity) as GameObject;
                meltParticle.transform.parent = this.transform;
            }
            if (laser.typeAttack == Laser.TypeAttack.Click)
            {
                time -= Time.deltaTime;
                if (time < 0)
                {
                    laserOn = false;
                    anim.SetBool("StartLaser", false);
                    time = 0.5f;
                }

            }
            for (int i = 0; i < hit.Length; i++)
            {
                for (int j = 0; j < hit.Length; j++)
                {
                    if (i == 0)
                    {
                        if (hit[i].collider != null)
                        {
                            if (hit[i].collider.tag == "Wall")
                            {
                                lineRenderer.SetPosition(0, rayBeginPos.position);
                                lineRenderer.SetPosition(1, hit[i].point);
                                laserHitEnd.transform.position = hit[i].point;
                                LaserColGlow(hit[i]);
                            }else if(hit[i].collider.tag =="Enemy")
                            {
                               
                                    if (hit[j].collider.tag == "Wall")
                                    {
                                        lineRenderer.SetPosition(0, rayBeginPos.position);
                                        lineRenderer.SetPosition(1, hit[j].point);
                                        laserHitEnd.transform.position = hit[j].point;
                                        LaserColGlow(hit[j]);
                                        if (hit[j - 1].collider.tag == "Wall")
                                        {
                                            lineRenderer.SetPosition(0, rayBeginPos.position);
                                            lineRenderer.SetPosition(1, hit[j - 1].point);
                                            laserHitEnd.transform.position = hit[j - 1].point;
                                            LaserColGlow(hit[j - 1]);
                                        }
                                    }
                                    else if (hit[j].collider.tag == "Enemy")
                                    {
                                    if (hitParticle[j] == null)
                                        {
                                            hitParticle[j] = Instantiate(laserHitEmitter, transform.position, Quaternion.identity);
                                        }

                                        if (hitParticle[j] != null)
                                        {
                                            hitParticle[j].transform.position = hit[j].point;
                                        }

                                        if(Time.time > lastShot+WeaponManager.Instance.current_Weapon.defaultConfig.fireRate)
                                    {
                                        int rd = Random.Range(0, 100);
                                        if (hit[j].collider.gameObject.GetComponent<EnemyController>())
                                        {
                                            if(rd<WeaponManager.Instance.current_Weapon.defaultConfig.critChance)
                                            {
                                                if(debuff!=null)
                                                {
                                                    var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
                                                    debuff1.GetComponent<Debuff>().Apply(hit[j].collider);
                                                }else
                                                {
                                                    hit[j].collider.gameObject.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage*2);
                                                }
                                                
                                            }else
                                            {
                                                hit[j].collider.gameObject.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                                            }
                                            
                                        }else if(hit[j].collider.gameObject.GetComponent<BossController>())
                                        {
                                            if(rd< WeaponManager.Instance.current_Weapon.defaultConfig.critChance)
                                            {
                                                if(debuff!=null)
                                                {
                                                    var debuff1 = ObjectPooling.Instance.GetGameObject(debuff, transform.position, Quaternion.identity);
                                                    debuff1.GetComponent<Debuff>().Apply(hit[j].collider);
                                                }else
                                                {
                                                    hit[j].collider.gameObject.GetComponent<BossController>().BossTakeDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage*2);
                                                }
                                                
                                            }else
                                            {
                                                hit[j].collider.gameObject.GetComponent<BossController>().BossTakeDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                                            }
                                            
                                        }
                                        
                                        lastShot = Time.time;
                                    }
                                        
                                    
                                }
                               
                            }
                            
                        }
                    }

                }


            }
        }
        else
        {
            for(int i=0;i<hitParticle.Length;i++)
            {
                if (hitParticle[i] != null)
                {
                    Destroy(hitParticle[i]);
                }
            }
            
            if (meltParticle != null)
                Destroy(meltParticle);

            TurnOfLaser();
            laserGlow.gameObject.SetActive(false);
        }
    }

    void FireLaser()
    {
        if(canFire)
        {
            anim.SetBool("StartLaser", true);
        }
    }

    RaycastHit2D LaserColGlow(RaycastHit2D hit)
    {
        if(!laserGlow.gameObject.activeInHierarchy)
        {
            laserGlow.gameObject.SetActive(true);
        }
        laserGlow.localScale = new Vector2(hit.distance*4f,laserGlow.localScale.y);
        return hit;
    }    

    float LerpLaser()
    {
        currentLerpTime += Time.deltaTime;

        if(currentLerpTime>lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float prec = currentLerpTime / lerpTime;
        return prec;
    }

    void TurnOfLaser()
    {
        Vector2 startPos = lineRenderer.GetPosition(0);
        Vector2 endPos = lineRenderer.GetPosition(1);

        length = (endPos - startPos).magnitude;

        float prec = LerpLaser();

        Vector2 lerp = Vector2.Lerp(endPos, startPos, prec);

        if (length > 0.3f)
        {
            lineRenderer.SetPosition(1, lerp);
        }
        else
        {
            lineRenderer.enabled = false;
            currentLerpTime = 0;
        }

    }

}
