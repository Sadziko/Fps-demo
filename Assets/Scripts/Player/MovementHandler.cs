using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField]private float gravityMultiplier = 1;

    private InputActionMap _movementActionMap;
    private CharacterController _characterController;
    private Camera _playerCam;
    private InputAction _movementInput;
    private Vector3 _playerVelocity;

    private Coroutine _continousShootingCoroutine;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _movementActionMap = GetComponent<PlayerInput>().actions.FindActionMap("Movement");
        _movementInput = GetComponent<PlayerInput>().actions.FindAction("BasicMovement");
        _playerCam = _player.PlayerCamera;
    }

    void Update()
    {
        Move();

        if(!_characterController.isGrounded)
            _playerVelocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
        
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _characterController.isGrounded)
        {
            _playerVelocity.y = 0f;
            _playerVelocity.y += jumpHeight;
        }
        
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartShooting();
        }

        if (context.canceled)
        {
            StopShooting();
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.GetCurrentWeapon().ZoomIn(_playerCam);
        }

        if (context.canceled)
        {
            _player.GetCurrentWeapon().ZoomOut(_playerCam);
        }
    }

    private void Move()
    {
        var playerInput = _movementInput.ReadValue<Vector2>();

        Vector3 groundMovement = transform.right * playerInput.x + transform.forward * playerInput.y;

        _playerVelocity.x = groundMovement.x * movementSpeed;
        _playerVelocity.z = groundMovement.z * movementSpeed;
    }

    private void StartShooting()
    {
        _continousShootingCoroutine = StartCoroutine(_player.GetCurrentWeapon().RapidFire());
    }

    private void StopShooting()
    {
        if(_continousShootingCoroutine != null)
            StopCoroutine(_continousShootingCoroutine);
    }

    public void EnableControls()
    {
        _movementActionMap.Enable();
    }

    public void DisableControls()
    {
        _movementActionMap.Disable();
    }
}
