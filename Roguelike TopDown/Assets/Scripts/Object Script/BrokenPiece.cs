using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    private float moveSpeed = 3f;

    private Vector3 moveDirection;

    private float deceleration=5f;

    private float lifeTime = 3f;

    private float fadeSpeed = 2.5f;

    private SpriteRenderer sr;

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }
    

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if(lifeTime <0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.MoveTowards(sr.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(sr.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
