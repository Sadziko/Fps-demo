using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public MovementHandler MovementHandler;
    public MouseLook MouseLook;
    
    public Camera PlayerCamera;
    public List<Weapon> WeaponsList;
    
    [SerializeField] private  CharacterController _characterController;
    private int _currentWeaponIndex;
    private Vector3 _startingPlayerPos;
    private Quaternion _startingRotation;
    
    private void Start()
    {
        SetCurrentWeaponToZero();
        _startingPlayerPos = transform.position;
        _startingRotation = transform.rotation;
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ChangeWeapon();
        }
    }
    
    public Weapon GetCurrentWeapon()
    {
        return WeaponsList[_currentWeaponIndex];
    }
    
    public void EnablePlayerControls()
    {
        MovementHandler.EnableControls();
        MouseLook.LockCursor();
    }
    
    public void DisablePlayerControls()
    {
        MovementHandler.DisableControls();
        MouseLook.UnlockCursor();
    }

    public void ResetPlayerPosition()
    {
        _characterController.enabled = false;
        gameObject.transform.SetPositionAndRotation(_startingPlayerPos,_startingRotation);
        _characterController.enabled = true;
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
    
    private void SetCurrentWeaponToZero()
    {
        _currentWeaponIndex = 0;
    }
}