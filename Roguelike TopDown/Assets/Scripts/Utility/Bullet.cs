using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isShooting { get; private set; }
    public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnDisable()
    {
        StopAllCoroutines();
        transform.ResetPosition();
        transform.ResetRotation();
        isShooting = false;
    }

    public void Shot(float speed, float angle, float accelSpeed, float accelTurn)
    {
        if (isShooting)
            return;

        isShooting = true;

        StartCoroutine(MoveCoroutine(speed, angle, accelSpeed, accelTurn));
    }
    
    
    IEnumerator MoveCoroutine(float speed,float angle, float accelSpeed,float accelTurn)
    {
            transform.SetEulerAnglesZ(angle);

        while(true)
        { 
                    float addAngle = accelTurn * Time.deltaTime;
                    transform.AddEulerAnglesZ(addAngle);
            speed += (accelSpeed * Time.deltaTime);

            transform.position += transform.right.normalized * speed * Time.deltaTime;
            yield return 0;
        }

        
    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

            StartCoroutine(Deactivate());
        
    }

    IEnumerator Deactivate()
    {
        yield return StartCoroutine(MyUtil.WaitForSeconds(0.1f));
        gameObject.SetActive(false);
    }

 

}
