using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<Damageable> targetsList;
    public GameObject WinScreenOverlay;
    public float TimeToRestart;

    private int _targetCount;
    private WaitForSeconds _restartWait;
    private Coroutine _restartCoroutine;

    private void Awake()
    {
        _restartWait = new WaitForSeconds(TimeToRestart);
    }

    private void Start()
    {
        var targets = FindObjectsOfType<Damageable>();
        targetsList.AddRange(targets);

        foreach (var damageable in targetsList)
        {
            damageable.Manager = this;
        }

        InitialSetup();
    }

    public void DeactivateTarget(Damageable target)
    {
        target.gameObject.SetActive(false);
        _targetCount--;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (_targetCount == 0)
        {
            Debug.Log("You win!");
            WinScreenOverlay.SetActive(true);
            _restartCoroutine = StartCoroutine(StartOverCoroutine());
        }
    }

    private IEnumerator StartOverCoroutine()
    {
        yield return _restartWait;
        InitialSetup();
    }

    private void InitialSetup()
    {
        if (_restartCoroutine != null)
            StopCoroutine(_restartCoroutine);
        
        WinScreenOverlay.SetActive(false);
        _targetCount = targetsList.Count;

        foreach (var target in targetsList)
        {
            target.gameObject.SetActive(true);
        }
    }
}