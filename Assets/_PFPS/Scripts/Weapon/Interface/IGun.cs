using UnityEngine;

namespace HeavenFalls
{
    public interface IGun : IWeapon
    {
        int CurrentAmmo { get; }
        int Magazine { get; }
        bool IsReloading { get; }
        Transform AimTarget { get; set; }
        void Reloading();
    }
}