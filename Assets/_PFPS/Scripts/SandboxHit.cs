using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class SandboxHit : MonoBehaviour
    {
        public WeaponGun weaponGun;
        
        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (!player) return;
            var weapon = Instantiate(weaponGun);
            // player.EquipWeapon(weapon);
        }
    }
}
