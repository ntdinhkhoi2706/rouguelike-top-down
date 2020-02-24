using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : SingletonMonoBehaviour<Timer>
{
    float lastTime;
    float deltaTime;
    float frameCount;
    bool Pausing;

    public float DeltaTime
    {
        get
        {
            return Pausing ? 0f : deltaTime;
        } 
    }

    public float FrameCount { get => frameCount; }

    protected override void Awake()
    {
        lastTime = Time.time;
        base.Awake();
    }

    void Update()
    {
        float nowTime = Time.time;
        deltaTime = nowTime - lastTime;
        lastTime = nowTime;

        if (!Pausing)
        {
            frameCount++;
        }
    }

    public void Pause()
    {
        Pausing = true;
    }

    public void Resume()
    {
        Pausing = false;
        lastTime = Time.time;
    }


}
