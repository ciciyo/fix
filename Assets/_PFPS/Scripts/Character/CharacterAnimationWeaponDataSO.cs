using System;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    [CreateAssetMenu(menuName = "Configuration/Character Animation Data")]
    public class CharacterAnimationWeaponDataSO : ScriptableObject
    {
        public List<string> rifle = new();
        public List<string> pistol = new();
        public List<string> melee = new();
    }
}