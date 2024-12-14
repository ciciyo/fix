using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class GrenadeBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionVfx;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction, float throwForce)
        {
            explosionVfx.Stop();
            _rb.velocity = direction * throwForce;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _rb.isKinematic = true;
                explosionVfx.Play();
                StartCoroutine(WaitVfx());
            }
        }

        IEnumerator WaitVfx()
        {
            yield return new WaitUntil(() => explosionVfx.isStopped);
            Destroy(gameObject);
        }
    }
}
