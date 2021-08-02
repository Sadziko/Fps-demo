using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Camera _playerCamera;
    [SerializeField] private float mouseSens;
    private InputAction _lookAction;
    private Vector2 _mousePosition;
    private float _camRot;

    private void Awake()
    {
        _lookAction = GetComponent<PlayerInput>().actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        Look();
    }
    
    void Look()
    {
        _mousePosition = _lookAction.ReadValue<Vector2>() * mouseSens * Time.deltaTime;

        _camRot -= _mousePosition.y;
        _camRot = Mathf.Clamp(_camRot, -90, 90);
        
        _playerCamera.transform.localRotation = Quaternion.Euler(_camRot, 0, 0);
        transform.Rotate(Vector3.up * _mousePosition.x);
    }
}
