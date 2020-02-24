using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T :MonoBehaviour
{
    static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();

                if(instance==null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }
            return instance;
        }
    }

    GameObject MyGameObject;
    Transform MyTransform;


    protected GameObject _GameObject
    {
        get
        {
            if(MyGameObject == null)
            {
                MyGameObject = this.gameObject;
            }
            return MyGameObject;
        }
    }

    protected Transform _Transform
    {
        get
        {
            if(MyTransform == null)
            {
                MyTransform = this.transform;
            }
            return MyTransform;
        }
    }

    protected virtual void Awake()
    {
        if(this != Instance)
        {
            GameObject obj = this.gameObject;
            Destroy(this);
            Destroy(obj);
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
