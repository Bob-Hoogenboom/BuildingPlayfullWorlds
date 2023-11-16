using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void OnMove(InputValue value)
    {
        print("get a move on");
        if (value == null) return;
        var moveValue = value.Get<Vector2>();
        _rb.velocity = moveValue * _speed * Time.deltaTime;


    }
}
