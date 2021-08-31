using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiTemporaryScreen : UiScreen
{
    public float ScreenTime = 3f;
    public UnityEvent onTimeComplete = new UnityEvent();

    private WaitForSeconds _waitForSeconds;
    private float _startTime;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(ScreenTime);
    }

    void Start()
    {
        base.Start();
        
        _startTime = Time.time;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForTime());
    }

    private IEnumerator WaitForTime()
    {
        yield return _waitForSeconds;

        if (onTimeComplete != null)
        {
            onTimeComplete.Invoke();
        }
    }
}
