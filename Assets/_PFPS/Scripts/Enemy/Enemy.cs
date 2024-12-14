using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace HeavenFalls
{
    public enum TargetAttack { Player, Bomb }
    
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterConfig config;
        [SerializeField] private float rotationSpeed = 5f;
        public float defense;
        [Header("Range Area")] 
        [SerializeField] private float attackRange = 14f;
        
        private IWeapon _weapon;

        private Animator _animator;
        private NavMeshAgent _agent;

        public Transform _targetAttack = null;
        private float _health;

        private TargetAttack _targetEnum;
        
        public event Action OnDeath;

        [Header("Agent")]
        [SerializeField] private float speed_walk = 2f;
        [SerializeField] private float speed_run = 4f;

        [Header("Dummy")]
        public GameObject alert;

        public float health => _health; 

        public NavMeshAgent agent => _agent;

        private void Awake()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            _animator = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public void setTarget(Transform target) => _targetAttack = target; 

        private void OnEnable()
        {
            if (GameModeInfiltrateBomb.IsBombActivate)
            {
                var randomTarget = Random.Range(0f, 1f);
                _targetEnum = randomTarget < 0.6f ? TargetAttack.Bomb : TargetAttack.Player;
                if (_targetEnum == TargetAttack.Player)
                {
                    _targetAttack = FindObjectOfType<Player>().transform;
                    attackRange = 14f;
                }
                else
                {
                    _targetAttack = FindObjectOfType<Bomb>().transform;
                    attackRange = 3f;
                }
            }
            else
            {
                _targetAttack = FindObjectOfType<Player>().transform;
            }

            if (_weapon is IGun gun)
            {
                gun.AimTarget = _targetAttack;
            }
            _health = config.Stats.health;
        }

        private void OnDisable()
        {
            OnDeath = null;
        }

        private void Update()
        {
            if(_targetAttack != null)
            {
                var distance = Vector3.Distance(transform.position, _targetAttack.position);

                _agent.destination = _targetAttack.position;
                _agent.isStopped = distance <= attackRange;

                _animator.SetBool("IsMoving", !_agent.isStopped);

                if (_agent.isStopped)
                {
                    RotateTowards(_targetAttack.position);
                    _weapon.Attack();
                }
                else
                {
                    _weapon.StopAttack();
                }
            }
            else
            {
                _weapon.StopAttack();
            }
            
        }
        
        private void RotateTowards(Vector3 targetPosition)
        {
            var directionToTarget = (targetPosition - transform.position).normalized;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            // Draw the attack range in the editor
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        public void TakeDamage(float damage)
        {
            float efectDamage = Mathf.Clamp(0, damage, (damage - defense));
            _health -= efectDamage;
            //_health -= damage;
            
            if (_health <= 0)
            {
                print("enemy death");
                OnDeath?.Invoke();
            }
        }


        public void SetAgentSpeedToWalk()
        {
            _agent.speed = speed_walk;
        }

        public void SetAgentSpeedToRun()
        {
            _agent.speed = speed_run;
        }


        #region alert
        public void ShowAlert()
        {
            StartCoroutine(OnShowAlert());
        }

        private IEnumerator OnShowAlert()
        {
            alert.SetActive(true);
            yield return new WaitForSeconds(3f);
            alert.SetActive(false);
        }

        #endregion

    }
}
