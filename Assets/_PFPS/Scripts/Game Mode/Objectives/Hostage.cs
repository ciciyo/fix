using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HeavenFalls
{
    public class Hostage : MonoBehaviour
    {
        [SerializeField] private float radius = 5f;
        [SerializeField] private float stopRange;
        [SerializeField] private LayerMask layerMask; 

        private Animator _animator;
        private NavMeshAgent _agent;

        private Transform _targetFollowed;
        private bool _isRescue;
        private readonly Collider[] _results = new Collider[5]; 

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _targetFollowed = FindObjectOfType<Player>().transform;
            _isRescue = false;
        }

        private void Update()
        {
            if (_isRescue) FollowTarget();
            else _animator.SetBool("IsMoving", true);
            
            InteractRescue();
            Rescued();
        }

        private void FollowTarget()
        {
            var targetPosition = _targetFollowed.position;
            var distance = Vector3.Distance(transform.position, targetPosition);
            
            _agent.destination = targetPosition;
            _agent.isStopped = distance <= stopRange; 
            
            _animator.SetBool("IsMoving", _agent.isStopped);
        }

        private void InteractRescue()
        {
            var sphereCenter = transform.position;
            var hitCount = Physics.OverlapSphereNonAlloc(sphereCenter, radius, _results, layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                var result = _results[i];
                
                if (_isRescue) return;
                
                if (result.TryGetComponent(out Player player))
                {
                    GameModeUI.OnEventRescue(true);
                    player.Rescue(() =>
                    {
                        _isRescue = true;
                        GameModeUI.OnEventRescue(false);
                    });
                }
                else
                {
                    GameModeUI.OnEventRescue(false);
                }
            }
        }

        private void Rescued()
        {
            var sphereCenter = transform.position;
            var hitCount = Physics.OverlapSphereNonAlloc(sphereCenter, 1, _results);

            for (int i = 0; i < hitCount; i++)
            {
                var result = _results[i];
                print(result.name);

                if (result.gameObject.name == "Rescue Point")
                {
                    GameModeRescueHostage.CountHostage();
                    Destroy(this.gameObject);
                }
            }
        }

        private void OnDrawGizmos()
        {
            // Draw the overlap sphere in the Scene view
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

