using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeavenFalls.Weapon
{

    public abstract class Weapon : MonoBehaviour
    {
        public WeaponDetail detail;
        
        public int currentAmmo { get; protected set; }
        
        [Tooltip("callback saat menembak. jummlah peluru , total_peluru")]
        public UnityAction<int, int> onFire;

        /// <summary>
        /// float: duration
        /// </summary>
        public UnityAction<float> onReload;
        public UnityAction<int, int> onReloadFinish;

        //nanti harus diperbaiki agar lebih dinamis
        [SerializeField] internal ConfigGun config;
        public abstract void Attack();
        public abstract void Reloading();
    }

    #region field
    public enum WeaponUseType
    {
        Shootable, Melee, Throwable
    }

    [System.Serializable]
    public struct WeaponDetail
    {
        public int id;
        public string name;
        [TextArea(3, 5)] public string description;
        public WeaponUseType UseType;
        public WeaponConfig config;


    }


    [System.Serializable]
    public struct WeaponStat
    {
        public float fireRate;
        public int damage;
        public int max_ammo;
        public float reloadTime;
        public float range;
        public float accuracy;

        public void AddStat(WeaponStat stat)
        {
            fireRate += stat.fireRate;
            damage += stat.damage;
            max_ammo += stat.max_ammo;
            reloadTime += stat.reloadTime;
            range += stat.range;
            accuracy += stat.accuracy;
        }

        public void SetStat(WeaponStat stat)
        {
            fireRate = stat.fireRate;
            damage = stat.damage;
            max_ammo = stat.max_ammo;
            reloadTime = stat.reloadTime;
            range = stat.range;
            accuracy = stat.accuracy;
        }
    }

    #endregion

}


