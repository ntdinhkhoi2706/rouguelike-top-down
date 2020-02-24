using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couroutine : MonoBehaviour
{
    public static Couroutine Instance;
    void Awake()
    {
        Instance = this;
    }

    public static Coroutine StartIE(IEnumerator routine)
    {
        return Instance.StartCoroutine(routine);
    }

}
