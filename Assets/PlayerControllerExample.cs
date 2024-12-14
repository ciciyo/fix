using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerExample : MonoBehaviour
{
    public FixedJoystick joystick;
    
    public float moveSpeed;
    
    public Transform groundCheck; 
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    
    private CharacterController _characterController;
    private bool _isGrounded;
    private Vector3 _velocity;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        var move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        
        _characterController.Move(move * (moveSpeed * Time.deltaTime));
    }
}
