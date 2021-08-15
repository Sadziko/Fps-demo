using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiSystem : MonoBehaviour
{
    public UiScreen StartScreen;
    
    public UnityEvent onSwitchScreen = new UnityEvent();
    
    private Component[] screens = new Component[0];
    private UiScreen _currentScreen;
    public UiScreen CurrentScreen
    {
        get { return _currentScreen; }
    }

    private UiScreen _previousScreen;
    public UiScreen PreviousScreen
    {
        get { return _previousScreen; }
    }
    
    void Start()
    {
        screens = GetComponentsInChildren<UiScreen>(true);

        if (StartScreen)
        {
            SwitchScreen(StartScreen);
        }
    }

    public void DisableScreen(UiScreen screen)
    {
        if (screen)
        {
            _previousScreen = screen;
            _currentScreen = null;
            screen.CloseScreen();
            screen.gameObject.SetActive(false);
        }
    }

    public void SwitchScreen(UiScreen screen)
    {
        if (screen)
        {
            if (_currentScreen)
            {
                _currentScreen.CloseScreen();
                _previousScreen = _currentScreen;
            }

            _currentScreen = screen;
            _currentScreen.gameObject.SetActive(true);
            _currentScreen.StartScreen();
            
            if(onSwitchScreen != null)
                onSwitchScreen.Invoke();
        }
    }

    public void GoToPreviousScreen()
    {
        if (_previousScreen)
        {
            SwitchScreen(_previousScreen);
        }
    }
}
