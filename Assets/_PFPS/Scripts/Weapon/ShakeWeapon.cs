using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class ShakeWeapon : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float amount;
        
        private void Update()
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.Sin(Time.time * speed) * amount,
                transform.localPosition.z
                );
        }
    }
}
