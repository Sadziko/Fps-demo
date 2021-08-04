using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UiScreen : MonoBehaviour
{
    public Selectable StartSelectable;

    [Header("Screen Events")] 
    public UnityEvent onScreenStart = new UnityEvent();
    public UnityEvent onScreenClose = new UnityEvent();
    
    void Start()
    {
        if (StartSelectable)
        {
            EventSystem.current.SetSelectedGameObject(StartSelectable.gameObject);
        }
        
    }
    
    public virtual void StartScreen()
    {
        if (onScreenStart != null)
        {
            onScreenStart.Invoke();
        }
    }

    public virtual void CloseScreen()
    {
        if (onScreenClose != null)
        {
            onScreenClose.Invoke();
        }
    }


}
