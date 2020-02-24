using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreakable : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brokenPieces=null;
    [SerializeField]
    private GameObject[] itemDrops=null;
    private float dropChance=5;

    private bool canDrop=true;
    private int maxPieces=5;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -1f);
        if(GameManager.Instance.mainBuff.Count>0)
        {
            foreach (var buff in GameManager.Instance.mainBuff)
            {
                if (buff.nameBuff.Contains("DropChance"))
                {
                    dropChance += 10;
                }
            }
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            AudioManager.Instance.PlaySFX(0);

            int piecesToDrop = Random.Range(1, maxPieces);

            for (int i = 0; i < piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, brokenPieces.Length);

                Instantiate(brokenPieces[randomPiece], transform.position, Quaternion.identity);
            }

            if (canDrop)
            {
                float rd = Random.Range(0f, 100f);
                if (rd < dropChance)
                {
                    int rdItem = Random.Range(0, itemDrops.Length);

                    Instantiate(itemDrops[rdItem], transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
}
