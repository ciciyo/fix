using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HeavenFalls.Weapon
{
    [CreateAssetMenu(menuName = "Configuration/Weapon/Weapon Master Data")]
    public class SO_WeaponData : ScriptableObject
    {
        public List<WeaponDetail> weapons;


        /*[Header("Weapons By Type")]
        public List<Weapon> weapons_gun;*/

        public WeaponDetail GetWeaponById(int id) => weapons.Find(x => x.id == id);



        /// <summary>
        /// digunakan di main menu untuk preview senjata berdasarkan jenisnya
        /// </summary>
        /// <param name="useType"></param>
        /// <returns></returns>
        public List<WeaponDetail> GetAllWeaponsByType(WeaponUseType useType)
        {
            return weapons.Where(weapon => weapon.UseType == useType).ToList();
        }

    }
}