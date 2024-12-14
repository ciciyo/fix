using System;
using UnityEngine;

namespace HeavenFalls
{
    [Serializable]
    public struct ConfigJump
    {
        public float force;
        public float gravity;
        public float groundCheckDistance;
        public LayerMask groundMask;
    }
}