using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject lineRendererPrefab=null;
    public GameObject lightRendererPrefab = null;

    
    [SerializeField]
    private GameObject laserMelEmitter = null;
    [SerializeField]
    private GameObject laserHitEmitter = null;
    [SerializeField]
    private Transform rayBeginPos = null;

    [SerializeField]
    private LayerMask enemyHitLayer=1;
    public bool laserOn=false;

    private float segmentLength = 1f;

    private Lightning LightningBolts;
    private Laser laser;

    private float lastShot;
    private float time=0.5f;
    GameObject meltParticle = null;
    GameObject[] hitParticle;


    void Awake()
    {
        hitParticle = new GameObject[100];
        LightningBolts = new Lightning(segmentLength);
        LightningBolts.Init(lineRendererPrefab, lightRendererPrefab,transform);
        laser = GetComponentInParent<Laser>();
    }

    public void BuildChain()
    {
        LightningBolts.Activate();
    }

    void Update()
    {

        if (laser.typeAttack == Laser.TypeAttack.Hold)
        {
            if (!PlayerController.Instance.CanShoot)
            {
                laserOn = false;
            }
        }
        if (laserOn)
        {
            Vector2 laserDir = transform.right;

            BuildChain();
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
                                LightningBolts.DrawLightning(rayBeginPos.transform.position, hit[i]);
                            }
                            else if (hit[i].collider.tag == "Enemy")
                            {
                                if (hit[j].collider.tag == "Wall")
                                {
                                    LightningBolts.DrawLightning(rayBeginPos.transform.position, hit[j]);
                                    if (hit[j - 1].collider.tag == "Wall")
                                    {
                                        LightningBolts.DrawLightning(rayBeginPos.transform.position, hit[j-1]);
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
                                    if (Time.time > lastShot + WeaponManager.Instance.current_Weapon.defaultConfig.fireRate)
                                    {
                                        if (laser.typeAttack == Laser.TypeAttack.Click)
                                        {
                                            if (hit[j].collider.gameObject.GetComponent<EnemyController>())
                                            {
                                                hit[j].collider.gameObject.GetComponent<EnemyController>().DealDamage(WeaponManager.Instance.current_Weapon.defaultConfig.damage);
                                            }
                                            else if (hit[j].collider.gameObject.GetComponent<BossController>())
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
            for (int i = 0; i < hitParticle.Length; i++)
            {
                if (hitParticle[i] != null)
                {
                    Destroy(hitParticle[i]);
                }
            }

            if (meltParticle != null)
                Destroy(meltParticle);

            LightningBolts.Deactivate();

        }

    }


}
