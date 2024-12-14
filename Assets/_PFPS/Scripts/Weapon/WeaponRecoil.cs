using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace HeavenFalls
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private bool isEnemyWeapon;
        [SerializeField] private CinemachineImpulseSource cameraImpulse;
        [HideInInspector] public Animator animator;

        private CinemachineFreeLook _cameraCinemachine;
        private Camera _cameraMain;

        public List<Vector2> recoilPatterns = new();
        private float _verticalRecoil;
        private float _horizontalRecoil;
        public float duration;

        private float _time;
        private int _index;

        private void Awake()
        {
            if (!isEnemyWeapon) _cameraCinemachine = FindObjectOfType<CinemachineFreeLook>();
            _cameraMain = Camera.main;
        }
        
        private void Update()
        {
            if (_time > 0)
            {
                _cameraCinemachine.m_YAxis.Value -= (_verticalRecoil / 1000 * Time.deltaTime) / duration;
                _cameraCinemachine.m_XAxis.Value -= (_horizontalRecoil / 10 * Time.deltaTime) / duration;
                _time -= Time.deltaTime;
            }
        }
        
        public void Generate(string weaponName)
        {
            _time = duration;
            
            cameraImpulse.GenerateImpulse(_cameraMain.transform.forward);

            _horizontalRecoil = recoilPatterns[_index].x;
            _verticalRecoil = recoilPatterns[_index].y;

            _index = NextIndex(_index);
            
            animator.Play("recoil_" + weaponName, 1, 0f);
        }

        public void Reset()
        {
            _index = 0;
        }

        private int NextIndex(int index)
        {
            return (index + 1) % recoilPatterns.Count;
        }
    }
}
