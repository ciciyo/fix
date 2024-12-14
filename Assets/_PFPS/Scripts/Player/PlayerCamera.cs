using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HeavenFalls
{
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineFreeLook _cmThirdPerson;
        private Camera _camera;
        
        // public CinemachineImpulseSource _cmImpulse;

        private bool _isThirdPerson;
        
        private float _xAxisValue;
        private float _yAxisValue;

        private float _xRotation;
        
        private Vector2 _smoothAimVelocity; 
        private Vector2 _currentAim;

        private void Awake()
        {
            _camera = Camera.main;
            _cmThirdPerson = FindObjectOfType<CinemachineFreeLook>();
        }

        public void PlayerFollowCamera(Transform character)
        {
            var cameraForward = _camera.transform.forward;
            cameraForward.y = 0;
            character.rotation = Quaternion.LookRotation(cameraForward);
        }

        public void ThirdPersonScope(bool isScope)
        {
            _cmThirdPerson.m_Lens.FieldOfView = isScope ? 35 : 60;
        }

        public void CameraShooting()
        {
            // _cmImpulse.GenerateImpulse();
        }
        
        public void CameraAxis(ConfigAim configAim, Vector2 input)
        {
            // Apply sensitivity
            _xAxisValue += input.x * configAim.lookSensitivity;
            _yAxisValue -= input.y * configAim.lookSensitivity; // Invert Y if necessary

            // Clamp Y axis to prevent over-rotation (optional)
            _yAxisValue = Mathf.Clamp(_yAxisValue, -configAim.limitLookY, configAim.limitLookY);

            // Set Cinemachine FreeLook axis values
            _cmThirdPerson.m_XAxis.Value = _xAxisValue;
            _cmThirdPerson.m_YAxis.Value = _yAxisValue;
        }
    }
}