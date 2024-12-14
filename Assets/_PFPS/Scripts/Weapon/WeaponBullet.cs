using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class WeaponBullet : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private SphereCollider _sphereCollider;
        private TrailRenderer _trailRenderer;
        [SerializeField] private float speed = 100f;
        public int Damage { get; set; }
        
        private void Awake()
        {
            _rigidBody = gameObject.AddComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.freezeRotation = true;
            
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            _sphereCollider.isTrigger = true;

            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnDisable()
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
            _trailRenderer.Clear();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidBody.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("hit player");
            }
        }
    }
}