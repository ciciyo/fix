using System;
using UnityEngine;

namespace HeavenFalls
{
    public class WeaponMelee : MonoBehaviour, IWeapon
    {
        [SerializeField] private ConfigMelee config;
        [SerializeField] private string weaponName;
        [SerializeField] private WeaponCategory weaponCategory;
        
        public GameObject GameObject => gameObject;
        public WeaponCategory GetCategory => weaponCategory;
        public string GetName => weaponName;

        public bool IsUsed
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void Attack()
        {
            PlayerWeapon.OnMeleeAttack(true);
        }

        public void StopAttack()
        {
            PlayerWeapon.OnMeleeAttack(false);
        }
    }
}