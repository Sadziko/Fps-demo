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
    void Start()
    {
        base.Start();

        _waitForSeconds = new WaitForSeconds(ScreenTime);
        _startTime = Time.time;
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
