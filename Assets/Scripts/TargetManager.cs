using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetManager : MonoBehaviour
{
    public List<Damageable> targetsList;
    public UnityEvent onWinCondition = new UnityEvent();

    private int _targetCount;

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
            onWinCondition.Invoke();
        }
    }

    public void InitialSetup()
    {
        _targetCount = targetsList.Count;

        foreach (var target in targetsList)
        {
            target.gameObject.SetActive(true);
        }
    }
}