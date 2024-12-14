using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMovement(float horizontal, float vertical)
        {
            _animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        }

        public void SetWalking(bool isWalking)
        {
            _animator.SetBool("IsWalking", isWalking);
        }

        public void SetJumping(bool isJumping)
        {
            _animator.SetBool("IsJumping", isJumping);
        }

        public void SetCrouching(bool isCrouching)
        {
            _animator.SetBool("IsCrouching", isCrouching);
        }
    }
}
