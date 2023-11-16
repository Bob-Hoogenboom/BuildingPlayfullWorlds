using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    
    private Transform _cam;
    private Vector3 _gravity;
    private float _turnSmoothVelocity;
    private CharacterController _charCon;
    private Vector2 _moveVector;

    private void Awake()
    {
        _charCon = GetComponent<CharacterController>();
        _cam = GameObject.FindWithTag("MainCamera").transform;
    }


    private void OnMovement(InputValue value)
    {
        _moveVector = value.Get<Vector2>();
    }


    private void Update()
    {
        Vector3 direction = new Vector3(_moveVector.x, 0f, _moveVector.y);

        if (!(direction.magnitude >= 0.1f)) return;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _charCon.Move(moveDir * speed * Time.deltaTime);

        ApplyGravity();
    }
    
    private void ApplyGravity()
    {
        _gravity += Physics.gravity * Time.deltaTime;
        _charCon.Move(_gravity);
        if (IsGrounded())
        {
            _gravity.y = 0;
        }
    }

    private bool IsGrounded() => _charCon.isGrounded;
}
