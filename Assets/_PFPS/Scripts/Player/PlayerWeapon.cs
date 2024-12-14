using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Space]
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Transform[] weaponSlots;
        
        private IWeapon[] _weapons;
        private IWeapon _activeWeapon;
        
        [SerializeField] private Animator animator;

        private Player _player;
        private GameplayHUDManager _hud;

        private static event Action<bool> EventMeleeAttack;

        private void MeleeAttack(bool isAttacking)
        {
            if (_activeWeapon == _weapons[(int)_activeWeapon.GetCategory])
            {
                animator.SetBool("MeleeAttack", isAttacking);
            }
        }
        
        public static void OnMeleeAttack(bool isAttacking)
        {
            EventMeleeAttack?.Invoke(isAttacking);
        }
        
        public Transform GetAimTarget => aimTarget;

        private void Awake()
        {
            _weapons = GetComponentsInChildren<IWeapon>();

            foreach (var weapon in _weapons)
            {
                print(weapon.GetName);
            }
            
            _player = GetComponent<Player>();
            _hud = FindObjectOfType<GameplayHUDManager>(true);
        }

        private void OnEnable()
        {
            EventMeleeAttack += MeleeAttack;
        }

        private void OnDisable()
        {
            EventMeleeAttack -= MeleeAttack;
        }

        private void Start()
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i] is IGun gun) gun.AimTarget = aimTarget;
                _weapons[i].GameObject.transform.parent = weaponSlots[i];
                _weapons[i].GameObject.transform.localPosition = Vector3.zero;
                _weapons[i].GameObject.transform.rotation = Quaternion.identity;
            }

            _activeWeapon = GetWeapon((int)WeaponCategory.Primary);
            SetActiveWeapon((int)WeaponCategory.Primary);
            if (_activeWeapon is IGun firearm)
            {
                _hud.UpdateAmmoText(firearm.CurrentAmmo, firearm.Magazine);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetActiveWeapon((int)WeaponCategory.Primary);
                if (_activeWeapon is IGun firearm)
                {
                    _hud.UpdateAmmoText(firearm.CurrentAmmo, firearm.Magazine);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetActiveWeapon((int)WeaponCategory.Secondary);
                if (_activeWeapon is IGun firearm)
                {
                    _hud.UpdateAmmoText(firearm.CurrentAmmo, firearm.Magazine);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetActiveWeapon((int)WeaponCategory.Melee);
            }
        }

        /*
        public void Unarmed()
        {
            handIK.weight = 0f;
            animator.SetLayerWeight(1, 0f);
        }
        */
        
        private IWeapon GetWeapon(int index)
        {
            if (index < 0 || index >= _weapons.Length)
            {
                return null;
            }

            return _weapons[index];
        }

        /*
        private void Equip(IWeapon weapon)
        {
            var activeWeaponCategoryIndex = (int)weapon.GetCategory;
            var selectedWeapon = GetWeapon(activeWeaponCategoryIndex);
            
            _activeWeapon = selectedWeapon;
            if (_activeWeapon is IGun gun) gun.AimTarget = aimTarget;
            _activeWeapon.GameObject.transform.parent = weaponSlots[activeWeaponCategoryIndex];
            _activeWeapon.GameObject.transform.localPosition = Vector3.zero;
            _activeWeapon.GameObject.transform.rotation = Quaternion.identity;

            // _weapons[activeWeaponCategoryIndex] = selectedWeapon;
            // _activeWeaponIndex = activeWeaponCategoryIndex;
        }
        */

        public void Attack()
        {
            _activeWeapon.Attack();
            if (_activeWeapon is IGun gun)
            {
                _hud.UpdateAmmoText(gun.CurrentAmmo, gun.Magazine);
            }
        }

        public void StopAttack()
        {
            _activeWeapon.StopAttack();
        }

        public void Reloading()
        {
            if (_activeWeapon is IGun gun)
            {
                gun.Reloading();
            }
        }
        
        private void SetActiveWeapon(int weaponSlotIndex)
        {
            _activeWeapon = _weapons[weaponSlotIndex];
            _activeWeapon.GameObject.GetComponent<WeaponGun>().recoil.animator = animator;
            StartCoroutine(SwitchWeapon((int)_activeWeapon.GetCategory, weaponSlotIndex));
        }
        
        private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
        {
            yield return StartCoroutine(HolsterWeapon(holsterIndex));
            yield return StartCoroutine(ActivateWeapon(activateIndex));
        }

        private IEnumerator HolsterWeapon(int index)
        {
            var weapon = GetWeapon(index);
            if (weapon != null)
            {
                animator.SetBool("IsHolsterWeapon", true);
                do
                {
                    yield return new WaitForEndOfFrame();
                } while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            }
        }

        private IEnumerator ActivateWeapon(int index)
        {
            var weapon = GetWeapon(index);
            if (weapon != null)
            {
                animator.SetBool("IsHolsterWeapon", false);
                animator.Play("weapon_" + _activeWeapon.GetName);
                do
                {
                    yield return new WaitForEndOfFrame();
                } while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            }
        }
    }
}