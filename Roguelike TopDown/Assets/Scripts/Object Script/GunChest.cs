using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponPickup = null;
    [SerializeField]
    private Sprite openChest = null;
    [SerializeField]
    private Transform spawnPoint = null;


    private float scaleSpeed = 2f;
    private bool canOpen, isOpen;

    private SpriteRenderer sr;

    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(JoyStickCanvas.Instance)
        {
            if (JoyStickCanvas.Instance.activity && !isOpen && canOpen)
            {
                int gunSelect = Random.Range(0, weaponPickup.Length);
                Instantiate(weaponPickup[gunSelect], spawnPoint.position, Quaternion.identity);

                sr.sprite = openChest;

                isOpen = true;

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                JoyStickCanvas.Instance.activity = false;
            }


            if (isOpen)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, scaleSpeed * Time.deltaTime);
                
            }
        }
        
}
    

    void OnCollisionStay2D(Collision2D target)
    {
        if(target.collider.tag == "Player" && !isOpen)
        {
            JoyStickCanvas.Instance.ChangeShotToActivity();
            canOpen = true;
        }
    }

    void OnCollisionExit2D(Collision2D target)
    {
        if (target.collider.tag == "Player" && isOpen || target.collider.tag == "Player" && !isOpen)
        {
            JoyStickCanvas.Instance.ChangeActivityToShot();
            canOpen = false;

        }
    }
}
