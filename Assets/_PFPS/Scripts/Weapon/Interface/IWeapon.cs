using UnityEngine;

namespace HeavenFalls
{
    public interface IWeapon
    {
        bool IsUsed { get; set; }
        void Attack();
        void StopAttack();
        string GetName { get; }
        GameObject GameObject { get; }
        WeaponCategory GetCategory { get; }
    }
}