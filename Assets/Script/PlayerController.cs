using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    [FormerlySerializedAs("playerVelocity")] [SerializeField]
    private Vector3 _playerVelocity;
    [FormerlySerializedAs("groundedPlayer")] [SerializeField]
    private bool _groundedPlayer;
    [FormerlySerializedAs("playerSpeed")] [SerializeField]
    private float _playerSpeed = 2.0f;
    [FormerlySerializedAs("jumpHeight")] [SerializeField]
    private float _jumpHeight = 1.0f;
    [FormerlySerializedAs("gravityValue")] [SerializeField]
    private float _gravityValue = -9.81f;
    [FormerlySerializedAs("rotationSpeed")] [SerializeField] private float _rotationSpeed = 4f;
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

        Vector2 movement = InputHandler.Instance.GetMovementInput();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = _cameraMainTransform.forward * move.z + _cameraMainTransform.right * move.x;
        move.y = 0f;
        _controller.Move(move * Time.deltaTime * _playerSpeed);

        // Makes the player jump
        if (InputHandler.Instance.IsJumping() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + _cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
