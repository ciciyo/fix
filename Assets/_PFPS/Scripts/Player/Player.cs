using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

namespace HeavenFalls
{
    /// <summary>
    /// Player View Setup
    /// </summary>
    
    [RequireComponent(typeof(PlayerCamera), typeof(PlayerController), typeof(PlayerWeapon))]
    public partial class Player : MonoBehaviour, IDamageable
    {
        public CharacterConfig characterConfig;

        private PlayerCamera _camera;
        private PlayerWeapon _weapon;
        private PlayerController _controller;

        private LogPlayer _logPlayer;
        private LogWeapon _logWeapon;

        private CharacterAnimator _characterAnimator;
        private CharacterWeaponIK _characterWeaponIK;

        public Weapon.Weapon weapon;
        private IMoveable _movement;

        private Vector3 _playerVelocityY;

        [HideInInspector] public float speed_crouch = 10;
        [HideInInspector] public float speed_run = 1;

        public float _moveSpeed;

        private float health_max;
        [SerializeField] private float _health;
        [SerializeField] private float m_defense_max;
        public float defense;
        
        private bool enableDamage = true;
        [Header("Ability")]
        public Ability ability;
        
        //Action
        public static event Action OnDeath;
        public UnityAction<float> onUpdateHealth;
        public UnityAction<float> onUpdateShield;

        //Coroutine
        private Coroutine co_shoot;
        
        private float max_shield;
        [SerializeField] private float _shield;

        //property
        public bool isShooting => co_shoot != null;
        public float defense_max => m_defense_max;
        
        private void Awake()
        {
            _movement = new CharacterMotionCC(gameObject, _playerVelocityY, (cc) =>
            {
                cc.height = characterConfig.GetSetup.height;
                cc.radius = characterConfig.GetSetup.radius;
                cc.minMoveDistance = 0;
            });

            _camera = GetComponent<PlayerCamera>();
            _controller = GetComponent<PlayerController>();
            _weapon = GetComponent<PlayerWeapon>();

            _characterWeaponIK = GetComponentInChildren<CharacterWeaponIK>();
            _characterAnimator = GetComponentInChildren<CharacterAnimator>();
            _controller.SetCursorHidden(true);

            _logPlayer = FindObjectOfType<LogPlayer>();
            _logWeapon = FindObjectOfType<LogWeapon>();

            _health = characterConfig.Stats.health;
            
            max_shield = characterConfig.Stats.shield;
            _shield = max_shield;

            health_max = characterConfig.Stats.health;
            m_defense_max = characterConfig.Stats.defense;
            defense = defense_max;
            _health = health_max;
            enableDamage = true;
            speed_run = characterConfig.Movement.run.force;
            speed_crouch = characterConfig.Movement.crouch.force;
        }

        private void OnEnable()
        {
            _controller.OnRunning += Running;
            _controller.OnWalking += Walking;
            _controller.OnCrouching += Crouching;
            _controller.OnJumping += Jumping;
            _controller.OnShooting += Shooting;
            _controller.OnStopShooting += StopShooting;
            _controller.OnReloading += Reloading;
            _controller.OnSwitchWeapon += SwitchWeapon;
            _controller.OnAbility += ActivateAbility;

            // controllerUI.btnSwitch.onClick.AddListener(SwitchWeapon);
            // controllerUI.btnJump.onClick.AddListener(Jumping);
            // controllerUI.btnCrouch.onClick.AddListener(Crouching);
            // controllerUI.btnScope.onClick.AddListener(Scoping);
            // controllerUI.btnReload.onClick.AddListener(Reloading);
            // controllerUI.btnShoot.EventHeld += Shooting;
            // controllerUI.btnShoot.EventUp += StopShooting;
            // controllerUI.btnDirectShoot.EventHeld += Shooting;
            // controllerUI.btnDirectShoot.EventUp += StopShooting;
        }

        private void Start()
        {
            _characterWeaponIK.SetAimTarget(new WeightedTransform(_weapon.GetAimTarget, 1.0f));
        }

        private void Update()
        {
            _movement.Move(Movement(_controller.MoveInput));
            _movement.ApplyGravity(characterConfig.Jump);

            // _camera.ThirdPersonAim(_input.AimAxis());
            _camera.PlayerFollowCamera(transform);
            _camera.ThirdPersonScope(_controller.IsScoping);

            _characterAnimator.SetCrouching(_controller.IsCrouching);
            _characterAnimator.SetJumping(!_movement.IsGrounded());
            _characterAnimator.SetMovement(_controller.MoveInput.x, _controller.MoveInput.z);
            
            _controller.HandleAction();

            // _logPlayer.Show(_controller.MoveInput, _movement.GetVelocity.sqrMagnitude, _movement.IsGrounded());
            if (_weapon is IGun gun)
            {
                _logWeapon.Show(gun.CurrentAmmo, gun.IsReloading);
            }
        }

        private void OnDisable()
        {
            _controller.OnRunning -= Running;
            _controller.OnWalking -= Walking;
            _controller.OnCrouching -= Crouching;
            _controller.OnJumping -= Jumping;
            _controller.OnShooting -= Shooting;
            _controller.OnStopShooting -= StopShooting;
            _controller.OnReloading -= Reloading;
            _controller.OnSwitchWeapon -= SwitchWeapon;
            _controller.OnAbility -= ActivateAbility;

            // controllerUI.btnSwitch.onClick.RemoveListener(SwitchWeapon);
            // controllerUI.btnJump.onClick.RemoveListener(Jumping);
            // controllerUI.btnCrouch.onClick.RemoveListener(Crouching);
            // controllerUI.btnScope.onClick.RemoveListener(Scoping);
            // controllerUI.btnReload.onClick.RemoveListener(Reloading);
            // controllerUI.btnShoot.EventHeld -= Shooting;
            // controllerUI.btnShoot.EventUp -= StopShooting;
            // controllerUI.btnDirectShoot.EventHeld -= Shooting;
            // controllerUI.btnDirectShoot.EventUp -= StopShooting;
        }

        public void TakeDamage(float damage)
        {
            if (!enableDamage) return;
            float efectDamage = Mathf.Clamp(0, damage, (damage - defense));

            if (_shield > 0)
            {
                _shield -= efectDamage;
                onUpdateShield?.Invoke(_shield / max_shield);

                //cek apakah shield bernilai negatif
                if(_shield < 0)
                {
                    _health -= Mathf.Abs(_shield);
                    _shield = 0;
                    onUpdateHealth?.Invoke(_health / health_max);
                }
            }
            else
            {
                _health -= efectDamage;
                onUpdateHealth?.Invoke(_health / health_max);

                if (_health <= 0)
                {
                    
                    //print("player death");
                    OnDeath?.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }

        public void AddHeal(float heal_value)
        {
            _health = Mathf.Clamp(_health + heal_value, 0, health_max);

            onUpdateHealth?.Invoke(_health / health_max);
        }

        public void Rescue(Action onRescue)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                onRescue();
            }
        }

        public void ActivateAbility()
        {
            if (ability) ability.Activate();
        }
        
        public void SetImunity(bool condition)
        {
            enableDamage = condition;
        }
    }

    /// <summary>
    /// Player Behavior.
    /// </summary>
    public partial class Player
    {
        private Vector3 Movement(Vector3 input)
        {
            return transform.right * (input.x * _moveSpeed) +
                   transform.forward * (input.z * _moveSpeed);
        }

        private void Walking()
        {
            _characterAnimator.SetWalking(true);
            _moveSpeed = speed_crouch;
        }

        private void Scoping()
        {
            _controller.IsScoping = !_controller.IsScoping;
        }

        private void Running()
        {
            _characterAnimator.SetWalking(false);
            _moveSpeed = speed_run;
        }

        private void Jumping()
        {
            _movement.Jump(characterConfig.Jump);
        }

        private void Crouching()
        {
            _moveSpeed = speed_crouch;
        }

        private void Shooting()
        {
            if (co_shoot != null)
            {
                StopCoroutine(co_shoot);
                co_shoot = null;
            }
            co_shoot = StartCoroutine(OnShooting());
        }

        private IEnumerator OnShooting()
        {
            if (_weapon != null) _weapon.Attack();
            // _camera.CameraShooting();

            //digunakan untuk keperluan ability ajax (mengetahui bahwa player melakukan shoot)
            yield return new WaitForEndOfFrame();
            co_shoot = null;
            yield break;
        }

        private void StopShooting()
        {
            if (_weapon != null) _weapon.StopAttack();
        }

        private void Reloading()
        {
            _weapon.Reloading();
        }

        private void SwitchWeapon()
        {
            // if (_weapons.Length <= 0) return;
            // _weaponCounter++;
            //
            // if (_weaponCounter >= _weapons.Length)
            // {
            //     _weaponCounter = 0;
            // }
            //
            // for (int i = 0; i < _weapons.Length; i++)
            // {
            //     _weapons[i].GameObject.SetActive(i == _weaponCounter);
            // }
            //
            // EquipWeapon(_weapons[_weaponCounter]);
        }
    }
}
