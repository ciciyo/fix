using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace HeavenFalls
{
    public class BombCountDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpStatus;
        [SerializeField] private TextMeshProUGUI tmpCountdown;

        public static event Action<string, string> EventCount;
        public static event Action<bool> EventShow;

        private void OnEnable()
        {
            EventShow += Show;
            EventCount += Count;
        }

        private void OnDisable()
        {
            EventShow -= Show;
            EventCount -= Count;
        }

        private void Count(string status, string count)
        {
            tmpStatus.text = status;
            tmpCountdown.text = count;
        }

        private void Show(bool isShow)
        {
            tmpStatus.gameObject.SetActive(isShow);
            tmpCountdown.gameObject.SetActive(isShow);
        }

        public static void OnCount(string status, string count)
        {
            EventCount?.Invoke(status, count);
        }

        public static void OnShow(bool isShow)
        {
            EventShow?.Invoke(isShow);
        }
    }
}