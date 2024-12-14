using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace HeavenFalls
{
    public class LaserGreen : Weapon.Weapon
    {
        private RaycastHit _hitInfo;
        private Ray _ray;
        [SerializeField] private LayerMask hit_layer;
        internal float duration = 5f;

        public GameObject laser;
        

        public override void Attack()
        {
            StartCoroutine(OnAttack());
        }

        private IEnumerator OnAttack()
        {
            laser.SetActive(true);
            yield return new WaitForSeconds(duration);
            laser.SetActive(false);
            yield break;
        }

        public override void Reloading()
        {
            return;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}