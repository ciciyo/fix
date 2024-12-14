using System;

namespace HeavenFalls
{
    [Serializable]
    public struct ConfigMovement
    {
        public ConfigCrouch crouch;
        public ConfigJump jump;
        public ConfigWalk walk;
        public ConfigRun run;
    }
}