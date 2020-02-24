using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float fireRate=0;
    [SerializeField]
    private BaseShot shot=null;

    private float fireCounter;
    private bool canShot = true;
   void OnEnable()
    {
        Destroy(gameObject, PlayerController.Instance.skillDuration);
    }

    void Update()
    {

        if(PlayerController.Instance.Enemy)
        {
            Vector3 targ = PlayerController.Instance.Enemy.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle1 = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle1));

            if (canShot)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    shot.Shot();
                    fireCounter = fireRate;
                    
                }
            }
        }
        
        
    }
}
