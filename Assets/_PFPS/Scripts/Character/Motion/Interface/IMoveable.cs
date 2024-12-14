using UnityEngine;

namespace HeavenFalls
{
    // Attach this for each moveable object.
    public interface IMoveable
    {
        void Move(Vector3 position);
        void Jump(ConfigJump config);
        void ApplyGravity(ConfigJump config);
        bool IsGrounded();
        Vector3 GetVelocity { get; }
    }
}