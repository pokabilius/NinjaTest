using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControllerNew : MonoBehaviour
{
    [SerializeField] private float _movespeed = 10f;
    [SerializeField] private float _jumpMultiplier = 10f;

    Rigidbody2D _rigidBody2D;
    Vector2 _movementForce;
    bool _jump;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _movementForce = new Vector2(horizontal, 0);

        if (Input.GetKey("space") && IsGrounded())
        {
            _jump = true;
        }
    }

    private bool IsGrounded() => true;

    private void FixedUpdate()
    {
        Move();
        Jump();
        
    }

    private void Jump()
    {
        if (_jump)
        {
            _rigidBody2D.AddForce(Vector2.up * _jumpMultiplier);
            _jump = false;
        }
    }

    private void Move()
    {
        if (Input.GetAxis("Horizontal") > 0 )
        {
            _rigidBody2D.AddForce(_movementForce * _movespeed);
            _animator.Play("Player_run");
            _spriteRenderer.flipX = false;

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _rigidBody2D.AddForce(_movementForce * _movespeed);
            _animator.Play("Player_run");
            _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.Play("idle");
        }
    }
}
