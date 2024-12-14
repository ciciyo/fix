using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HeavenFalls;
using Unity.Mathematics;
using UnityEngine;

namespace HeavenFalls
{
    public class CommsTower : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] private ParticleSystem particleSystem;
        public float duration;
        public Vector3 shake;

        public event Action OnDeath;
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            transform.DOShakePosition(duration, shake);
            
            if (health <= 0)
            {
                var explosion = Instantiate(particleSystem, transform.position, Quaternion.identity);
                explosion.Play();
                gameObject.SetActive(false);
                OnDeath?.Invoke();
            }
        }
    }
}
