using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public TMP_Text GameTimerText;

    [SerializeField] private TimerController _timerController;
    
    void Start()
    {
        GameTimerText.text = "0:00:00";
    }

    void Update()
    {
        GameTimerText.text = _timerController.Timer.ToString("mm':'ss'.'fff");
    }
}
