using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class WeaponGun : Weapon.Weapon, IGun
    {
        //[SerializeField] private ConfigGun config;
        [SerializeField] private LayerMask hit_layer;
        
        [Space]
        [SerializeField] private string weaponName;
        [SerializeField] private WeaponCategory weaponCategory;
        [SerializeField] private bool isEnemyWeapon; // True if used by enemy
        [SerializeField] private float enemyRecoilOffset;
        
        public WeaponRecoil recoil;
        
        public GameObject GameObject => gameObject;
        public WeaponCategory GetCategory => weaponCategory;
        public string GetName => weaponName;

        private Coroutine _autoFireCoroutine;
        private RaycastHit _hitInfo;
        private Ray _ray;
        
        public int CurrentAmmo { get; private set; }
        public int Magazine => config.gunProperties.maxAmmoMagazine;
        public bool IsReloading { get; private set; }
        public Transform AimTarget { get; set; }
        private float _nextFireTime;
        private Transform _target;

        private void Start()
        {
            _nextFireTime = 0f;
            CurrentAmmo = config.gunProperties.maxAmmoMagazine;
            currentAmmo = config.gunProperties.maxAmmoMagazine;
        }

        public bool IsUsed
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public override void Attack()
        {
            if (IsReloading)
            {
                return; // Do nothing if reloading
            }

            if (!isEnemyWeapon) recoil.Reset();
            
            if (config.gunProperties.weaponType == WeaponType.Automatic)
            {
                if (_autoFireCoroutine == null)
                {
                    _autoFireCoroutine = StartCoroutine(AutoFire());
                }
            }
            else
            {
                if (Time.time >= _nextFireTime)
                {
                    Fire();
                    _nextFireTime = Time.time + config.gunProperties.fireRate;
                }
            }
        }

        private void Fire()
        {
            if (IsReloading || CurrentAmmo <= 0)
            {
                return; // Skip firing if reloading or no ammo
            }

            var shootOrigin = config.gunProperties.shootSpawn.position;
            
            if (isEnemyWeapon) ApplyEnemyRecoil();
            
            _ray.origin = shootOrigin;
            _ray.direction = AimTarget.position - shootOrigin;

            var trail = Instantiate(config.vfxProperties.trailEffect, _ray.origin, Quaternion.identity);
            trail.AddPosition(_ray.origin);
            
            if (Physics.Raycast(_ray, out _hitInfo, 25, hit_layer))
            {
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.magenta, 0.5f);
                
                var hitEffectTransform = config.vfxProperties.hitEffect.transform;
                hitEffectTransform.position = _hitInfo.point;
                hitEffectTransform.forward = _hitInfo.normal;
                
                config.vfxProperties.hitEffect.Emit(1);
                trail.transform.position = _hitInfo.point;
                
                if (_hitInfo.transform.TryGetComponent<IDamageable>(out var damageable))
                {
                    //print(_hitInfo.transform.name);
                    damageable.TakeDamage(config.gunProperties.damage);
                }
            }
            else
            {
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.magenta, 0.5f);
                trail.transform.position = AimTarget.position; 
            }

            foreach (var muzzle in config.vfxProperties.muzzleFlash)
            {
                muzzle.Emit(1);
            }

            CurrentAmmo--;

            onFire?.Invoke(CurrentAmmo, config.gunProperties.maxAmmoMagazine);

            if (!isEnemyWeapon)
            {
                print("generate recoil player");
                recoil.Generate(weaponName);
            }
            
            if (CurrentAmmo <= 0)
            {
                Reloading();
            }
        }

        public override void Reloading()
        {
            if (IsReloading || CurrentAmmo == config.gunProperties.maxAmmoMagazine)
            {
                return;
            }
            
            IsReloading = true;
            StopAttack();
            if(gameObject.activeInHierarchy)StartCoroutine(ReloadRoutine());
        }

        private IEnumerator ReloadRoutine()
        {
            if (!gameObject.activeSelf) yield break;
            onReload?.Invoke(config.gunProperties.reloadTime);
            var elapsedTime = 0f;
    
            while (elapsedTime < config.gunProperties.reloadTime)
            {
                if (!gameObject.activeSelf) yield break;
        
                yield return null;
                elapsedTime += Time.deltaTime;
            }
    
            CurrentAmmo = config.gunProperties.maxAmmoMagazine;
            IsReloading = false;
            onReloadFinish?.Invoke(CurrentAmmo, CurrentAmmo);
        }
        
        private IEnumerator AutoFire()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(config.gunProperties.fireRate);
            }
        }
        
        public void StopAttack()
        {
            if (_autoFireCoroutine != null)
            {
                StopCoroutine(_autoFireCoroutine);
                _autoFireCoroutine = null;
            }
        }
        
        private void ApplyEnemyRecoil()
        {
            // Slightly adjust the aim target to simulate recoil
            var aimOffset = new Vector3(
                UnityEngine.Random.Range(-enemyRecoilOffset, enemyRecoilOffset),
                UnityEngine.Random.Range(-enemyRecoilOffset, enemyRecoilOffset),
                0f
            );
            
            var temporaryTarget = AimTarget.position + aimOffset;
            _ray.origin = config.gunProperties.shootSpawn.position;
            _ray.direction = (temporaryTarget - _ray.origin).normalized;
        }
    }
}