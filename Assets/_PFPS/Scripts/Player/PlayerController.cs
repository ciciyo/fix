using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerControllerUI controllerUI;
        
        public bool IsCrouching { get; set; }
        public bool IsScoping { get; set; }

        public event Action OnRunning,
            OnWalking,
            OnJumping,
            OnCrouching,
            OnShooting,
            OnStopShooting,
            OnReloading,
            OnSwitchWeapon,
            OnAbility;

        public void SetCursorHidden(bool isHiding)
        {
            if (isHiding)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public Vector3 MoveInput
        {
            get
            {
    #if UNITY_EDITOR
                var moveHorizontal = Input.GetAxisRaw("Horizontal");
                var moveVertical = Input.GetAxisRaw("Vertical");
                // var moveHorizontal = _moveJoystick.Horizontal;
                // var moveVertical = _moveJoystick.Vertical;
    #elif UNITY_ANDROID || UNITY_IPHONE
                var moveHorizontal = controllerUI.GetMovementInput.x;
                var moveVertical = controllerUI.GetMovementInput.y;
    #endif
                return new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
            }
        }

        public Vector2 AimInput
        {
            get 
            {
    #if UNITY_EDITOR
                var xInput = Input.GetAxis("Mouse X");
                var yInput = Input.GetAxis("Mouse Y");
                // var xInput = _aimJoystick.TouchDist.x;
                // var yInput = _aimJoystick.TouchDist.y;
    #elif UNITY_ANDROID || UNITY_IPHONE
                var xInput = controllerUI.GetAimInput.x;
                var yInput = controllerUI.GetAimInput.y;
    #endif

                var x = Mathf.Clamp(xInput, 0, 1);
                var y = Mathf.Clamp(yInput, 0, 1);

                return new Vector2(x, y);
            }
        }

        public void HandleAction()
        {
            if (Input.GetMouseButtonDown(1))
            {
                IsScoping = !IsScoping;
            }

            if (Input.GetMouseButton(0))
            {
                OnShooting?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnStopShooting?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                OnReloading?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                OnAbility?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                OnSwitchWeapon?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJumping?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                OnWalking?.Invoke();
            }
            else
            {
                OnRunning?.Invoke();   
            }

            if (SetCrouching())
            {
                OnCrouching?.Invoke();
            }
        }

        // private void Shooting()
        // {
        //     OnShooting?.Invoke();
        // }
        //
        // private void StopShooting()
        // {
        //     OnStopShooting?.Invoke();
        // }

        private bool SetCrouching()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                IsCrouching = !IsCrouching;
            }

            return IsCrouching;
        }
    }
}
