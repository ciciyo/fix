using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeavenFalls
{
    public class PlayerControllerUI : MonoBehaviour
    {
        public Button btnJump;
        public Button btnCrouch;
        public ButtonUtility btnDirectShoot;
        public ButtonUtility btnShoot;
        public Button btnThrowable;
        public Button btnAbility;
        public Button btnScope;
        public Button btnReload;
        public Button btnSwitch;
        public Button btnHome;
        
        [SerializeField] private FixedJoystick moveJoystick;
        [SerializeField] private FixedTouchField aimJoystick;

        public Vector2 GetMovementInput => new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
        public Vector2 GetAimInput => aimJoystick.TouchDist;
    }
}
