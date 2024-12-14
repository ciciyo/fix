using System;
using UnityEngine;

namespace HeavenFalls
{
    public class CharacterMotionRB : IMoveable
    {
        private Rigidbody _rigidBody;
        private CapsuleCollider _capsuleCollider;
        private readonly ConfigJump _configJump;
        
        public CharacterMotionRB(GameObject gameObject, ConfigJump config, Action<Rigidbody, CapsuleCollider> callbackSetup)
        {
            InitializeComponent(gameObject);
            callbackSetup(_rigidBody, _capsuleCollider);
            _configJump = config;
        }
        
        private void InitializeComponent(GameObject obj)
        {
            _rigidBody = obj.AddComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.freezeRotation = true;

            _capsuleCollider = obj.AddComponent<CapsuleCollider>();
        }

        public void ApplyGravity(ConfigJump config)
        {
            // choose between fixed delta time for smooth gravity landing,
            // or using the rigid body mass for rough landing.
            var gravityForce = _rigidBody.transform.up * (-config.gravity * Time.fixedDeltaTime);
            _rigidBody.AddForce(gravityForce, ForceMode.Impulse);
        }

        public bool IsGrounded()
        {
            var grounded = Physics.Raycast(
                    _rigidBody.transform.position,
                    Vector3.down,
                    _configJump.groundCheckDistance,
                    _configJump.groundMask
            );
            return grounded;
        }

        public Vector3 GetVelocity => _rigidBody.velocity;

        public void Move(Vector3 position)
        {
            position.y = _rigidBody.velocity.y;
            _rigidBody.velocity = position;
            IsGrounded();
        }

        public void Jump(ConfigJump config)
        {
            if (IsGrounded())
            {
                _rigidBody.AddForce(_rigidBody.transform.up * config.force, ForceMode.Impulse);
            }
        }
    }
}