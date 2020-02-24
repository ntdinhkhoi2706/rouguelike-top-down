using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couroutine : SingletonMonoBehaviour<Couroutine>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public static Coroutine StartIE(IEnumerator routine)
    {
        return Instance.StartCoroutine(routine);
    }

}
