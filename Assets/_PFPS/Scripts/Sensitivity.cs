using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeavenFalls
{
    public class Sensitivity : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpSensitivity;
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private PlayerAim playerAim;

        private void Start()
        {
            tmpSensitivity.text = "Sensitivity " + playerAim.sensitivity.ToString("F");
            sensitivitySlider.value = playerAim.sensitivity;
            sensitivitySlider.onValueChanged.AddListener(delegate
            {
                playerAim.sensitivity = sensitivitySlider.value;
                tmpSensitivity.text = "Sensitivity " + playerAim.sensitivity.ToString("F");
            });
        }
    }
}
