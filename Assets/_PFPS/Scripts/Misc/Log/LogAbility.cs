using System;
using TMPro;
using UnityEngine;

namespace HeavenFalls
{
    public class LogAbility : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpDescription;

        private static event Action<string> EventLog;

        private void OnEnable()
        {
            EventLog += UpdateLog;
        }

        private void OnDisable()
        {
            EventLog -= UpdateLog;
        }

        private void UpdateLog(string message)
        {
            tmpDescription.text = "Description: " + message;
        }

        public static void OnEventLog(string obj)
        {
            EventLog?.Invoke(obj);
        }
    }
}