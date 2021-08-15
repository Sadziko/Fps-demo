using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TimeSpan Timer { get; private set; }

    private float _elapsedTime;
    private bool _isRunning;
    

    void Update()
    {
        if (_isRunning)
        {
            _elapsedTime += Time.deltaTime;
            Timer = TimeSpan.FromSeconds(_elapsedTime);
        }
    }

    public void StartTimer()
    {
        ResetTimer();
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _isRunning = false;
        _elapsedTime = 0f;
    }
}