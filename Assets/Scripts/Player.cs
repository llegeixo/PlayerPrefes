using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController _controller;
    Animator _animator;
    Transform _camera;

    [SerializeField] float _playerSpeed = 5;
    [SerializeField] float _jumpHeight = 5;

    float _horizontal;
    float _vertical;

    float _turnSmoothTime;
    float _turnSmoothVelocity;

    float _gravity = -9.81f;
    Vector3 _playerGravity;

    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius = 0.2f;
    [SerializeField] LayerMask _groundLayer;
    bool _isGrounded;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main.transform;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement();
        Jump();
    }

    void Movement()
    {
       Vector3 direction = new Vector3(_horizontal, 0, _vertical);

       _animator.SetFloat("VelX", 0);
       _animator.SetFloat("VelZ", direction.magnitude);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, _smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
            _animator.SetBool("IsJumping", false);
        }
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
            _animator.SetBool("IsJumping", true);
        }
        _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }
}
