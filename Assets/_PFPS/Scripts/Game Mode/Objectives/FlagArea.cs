using System;
using UnityEngine;

namespace HeavenFalls
{
    public class FlagArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}