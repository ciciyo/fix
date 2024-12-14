using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class LogGameMode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpTimer;
        [SerializeField] private TextMeshProUGUI tmpObjective;
        [SerializeField] private TextMeshProUGUI tmpRescue;

        private static event Action<string, string> EventUpdate;
        private static event Action<string> EventTimeUpdate;
        private static event Action<bool> EventRescue;

        private void OnEnable()
        {
            EventUpdate += UpdateLog;
            EventTimeUpdate += OnUpdateTime;
            EventRescue += OnRescue;
        }

        private void OnDisable()
        {
            EventUpdate -= UpdateLog;
            EventTimeUpdate -= OnUpdateTime;
            EventRescue -= OnRescue;
        }

        private void UpdateLog(string counterTime, string counterObjective)
        {
            tmpTimer.text = counterTime;
            tmpObjective.text = counterObjective;
        }

        private void OnUpdateTime(string time)
        {
            tmpTimer.text = time;
        }

        private void OnRescue(bool isShow)
        {
            tmpRescue.gameObject.SetActive(isShow);
        }

        public static void OnUpdate(string counterTime, string counterKill)
        {
            EventUpdate?.Invoke(counterTime, counterKill);
        }

        public static void OnEventTimeUpdate(string time)
        {
            EventTimeUpdate?.Invoke(time);
        }

        public static void OnEventRescue(bool isShow)
        {
            EventRescue?.Invoke(isShow);
        }
    }
}