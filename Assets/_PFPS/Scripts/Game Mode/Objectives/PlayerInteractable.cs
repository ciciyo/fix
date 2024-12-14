using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class PlayerInteractable : MonoBehaviour
    {
        private CharacterController _cc;
        public float radiusMultiplier = 1.1f;

        private readonly Collider[] _results = new Collider[3];

        private void Start()
        {
            _cc = GetComponent<CharacterController>();

            if (_cc == null)
            {
                Debug.LogError("No CharacterController found on this GameObject.");
            }
        }

        private void Update()
        {
            if (_cc == null) return;
            
            var point0 = transform.position + _cc.center + Vector3.up * (_cc.height / 2 - _cc.radius);
            var point1 = transform.position + _cc.center - Vector3.up * (_cc.height / 2 - _cc.radius);

            var radius = _cc.radius * radiusMultiplier;

            var hitCount = Physics.OverlapCapsuleNonAlloc(
                point0,
                point1,
                radius,
                _results
            );

            for (int i = 0; i < hitCount; i++)
            {
                var col = _results[i];
                if (col.CompareTag("FlagObject") && !GameModeCaptureFlag.isHasFlag)
                {
                    CollectFlag(col);
                } 
                else if (col.CompareTag("FlagArea") && GameModeCaptureFlag.isHasFlag)
                {
                    GameModeCaptureFlag.OnFlagReturned?.Invoke();
                }
            }
        }

        private static void CollectFlag(Component col)
        {
            col.gameObject.SetActive(false);
            GameModeCaptureFlag.OnFlagCaptured?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (_cc == null) _cc = GetComponent<CharacterController>();
            if (_cc == null) return;
            
            // Calculate capsule points and radius for Gizmos
            var point0 = transform.position + _cc.center + Vector3.up * (_cc.height / 2 - _cc.radius);
            var point1 = transform.position + _cc.center - Vector3.up * (_cc.height / 2 - _cc.radius);
            var radius = _cc.radius * radiusMultiplier;

            // Draw the capsule
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point0, radius);
            Gizmos.DrawWireSphere(point1, radius);
            Gizmos.DrawLine(point0 + Vector3.up * radius, point1 + Vector3.up * radius);
            Gizmos.DrawLine(point0 - Vector3.up * radius, point1 - Vector3.up * radius);
            Gizmos.DrawLine(point0 + Vector3.right * radius, point1 + Vector3.right * radius);
            Gizmos.DrawLine(point0 - Vector3.right * radius, point1 - Vector3.right * radius);
        }
    }
}
