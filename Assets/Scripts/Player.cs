using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera PlayerCamera;
    public List<Weapon> WeaponsList;
    private int _currentWeaponIndex;
    
    private void Start()
    {
        SetCurrentWeaponToZero();
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        _currentWeaponIndex++;
        if (_currentWeaponIndex > WeaponsList.Count - 1)
            SetCurrentWeaponToZero();

        if (_currentWeaponIndex - 1 < 0) //Handling going from last to first index of list
        {
            WeaponsList[WeaponsList.Count - 1].gameObject.SetActive(false);
        }
        else
        {
            WeaponsList[_currentWeaponIndex - 1].gameObject.SetActive(false);
        }
        WeaponsList[_currentWeaponIndex].gameObject.SetActive(true);
    }

    public Weapon GetCurrentWeapon()
    {
        return WeaponsList[_currentWeaponIndex];
    }

    private void SetCurrentWeaponToZero()
    {
        _currentWeaponIndex = 0;
    }
}