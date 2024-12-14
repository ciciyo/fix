using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    [Serializable]
    public struct ConfigGun
    {
        public GunProperties gunProperties;
        public GunVisualEffects vfxProperties;
    }

    [Serializable]
    public struct GunProperties
    {
        public WeaponType weaponType;
        public float fireRate;
        public int damage;
        public int maxAmmoMagazine;
        public float reloadTime;
        public Transform shootSpawn;
    }
    
    [Serializable]
    public struct GunVisualEffects
    {
        public ParticleSystem[] muzzleFlash;
        public ParticleSystem hitEffect;
        public TrailRenderer trailEffect;
    }

    [Serializable]
    public enum WeaponType
    {
        Automatic,
        SemiAutomatic
    }

    
}