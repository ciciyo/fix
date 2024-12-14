using System;
using UnityEngine;

namespace HeavenFalls
{
    public class CharacterMotionCC : IMoveable
    {
        private const float DefaultJumpGrounding = -0.1f;
        
        private CharacterController _characterController;
        private CollisionFlags _collisionFlags;
        private Vector3 _velocityY;
        
        public CharacterMotionCC(GameObject gameObject, Vector3 velocityY, Action<CharacterController> callbackSetup)
        {
            InitializeComponent(gameObject);
            callbackSetup(_characterController);
            _velocityY = velocityY;
        }
        
        private void InitializeComponent(GameObject obj)
        {
            _characterController = obj.AddComponent<CharacterController>();
        }

        public Vector3 GetVelocity => _characterController.velocity;

        public void Move(Vector3 position)
        {
            _characterController.Move(position * Time.deltaTime);
        }
        
        public void ApplyGravity(ConfigJump config)
        {
            if (IsGrounded() && _velocityY.y < 0)
            {
                _velocityY.y = DefaultJumpGrounding;
            }
            
            _velocityY.y -= config.gravity * 2 * Time.deltaTime;
            _collisionFlags = _characterController.Move(_velocityY * Time.deltaTime);
            IsGrounded();
        }
        
        public void Jump(ConfigJump config)
        {
            if (IsGrounded())
            {
                _velocityY.y = Mathf.Sqrt(config.force * config.gravity);
            }
        }

        public bool IsGrounded()
        {
            return (_collisionFlags & CollisionFlags.Below) != 0;
        }
    }
}