using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;


namespace PlexusTechdev.Utility
{
    public class NotificationDisplay : MonoBehaviour
    {
        public TextMeshProUGUI message_text;
        public CanvasGroup canvasGroup;
        public float showDuration = 2f;

        private void Start()
        {
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) ShowNotif("notifikasi baru untuk tes");
        }*/

        public void ShowNotif(string message)
        {
            StartCoroutine(OnShowNotif(message));
        }

        public IEnumerator OnShowNotif(string message)
        {
            message_text.text = message;
            canvasGroup.DOFade(1, 0.5f);
            yield return new WaitForSeconds(showDuration);
            canvasGroup.DOFade(0, 0.5f);

            yield break;
        }

    }
}

