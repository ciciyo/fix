using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


namespace PlexusTechdev.Utility.ScreenManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        [Header("Do OnEnable")]
        public UnityEvent enable;

        [Header("Do OnDisable")]
        public UnityEvent disable;

        public string screenName;

        private void Start()
        {
            if(canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        }


        public void Show()
        {
            canvasGroup.DOFade(1, 0.5f);
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, 0.5f);
        }

        private void OnEnable()
        {
            enable.Invoke();
        }

        private void OnDisable()
        {
            disable.Invoke();
        }
    } 
}