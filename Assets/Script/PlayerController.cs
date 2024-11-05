using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private Vector3 _playerVelocity;
    [SerializeField]
    private bool _groundedPlayer;
    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _jumpHeight = 1.0f;

    [SerializeField] private float _sprintMultiplier = 1.5f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    [SerializeField] private float _rotationSpeed = 4f;
    private Transform _cameraMainTransform;
    private void Start()
    {
        _cameraMainTransform = Camera.main.transform;
        _controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector2 movementInput = InputHandler.Instance.GetMovementInput();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 forward = InputHandler.IsRTSCameraMode ? _cameraMainTransform.up : _cameraMainTransform.forward;            // Ensures that the player moves correctly across different cameras.
        Vector3 right = _cameraMainTransform.right;

        forward.y = 0;                                                                                                      // Prevents unwanted jumping effect.
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        float _targetSpeed = InputHandler.Instance.IsSprinting() ? _sprintMultiplier * _playerSpeed : _playerSpeed;

        Vector3 movement = (forward * move.z + right * move.x).normalized;
        _controller.Move(movement * Time.deltaTime * _targetSpeed);

        if (InputHandler.Instance.IsJumping() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        if (!InputHandler.IsRTSCameraMode)                                                                                   // In FPS and Third Person, we want the player to rotate to where we are looking at.
        {
            Quaternion targetRotation = Quaternion.Euler(0, _cameraMainTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        } else                                                                                                               // In RTS, the player doesn't have to rotate based on where the camera is looking at.
        {
            if (movementInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}
