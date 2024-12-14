using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class CharacterWeaponIK : MonoBehaviour
    {
        [Header("Base Rig Setting")]
        [SerializeField] private RigBuilder rig;
        [SerializeField] private MultiAimConstraint weaponAimConstraint, headAimConstraint;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void SetAimTarget(WeightedTransform target)
        {
            var sourceObjects = new WeightedTransformArray { target };
            headAimConstraint.data.sourceObjects = sourceObjects;
            weaponAimConstraint.data.sourceObjects = sourceObjects;
            rig.Build();

            Debug.Log("Source objects assigned successfully!");
        }
    }
}
