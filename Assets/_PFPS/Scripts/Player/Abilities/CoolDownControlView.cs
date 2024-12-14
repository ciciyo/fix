using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownControlView : MonoBehaviour
{
    const float COOLDOWNTIME_DEFAULT = 10f;
    public TMP_Text timer_text;
    public Image fill_image;

    private Coroutine co_cooldown;
    private Coroutine co_reduceFill;
    public bool isCoolDown => co_cooldown != null;




    #region cooldown
    public void CoolDown(float duration = COOLDOWNTIME_DEFAULT)
    {
        if (co_reduceFill != null) StopCoroutine(co_reduceFill);

        if (isCoolDown)
        {
            StopCoroutine(co_cooldown);
            co_cooldown = null;
        }

        co_cooldown = StartCoroutine(OnCoolDown(duration));
    }


    private IEnumerator OnCoolDown(float duration)
    {
        float time_remaining = duration;
        fill_image.fillAmount = (duration - time_remaining)/duration;
        int minutes = (int)(time_remaining / 60);
        int seconds = (int)(time_remaining % 60);

        // Format waktu dengan 2 digit angka
        if (timer_text) timer_text.text = $"{minutes:00}:{seconds:00}";

        while (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            fill_image.fillAmount = (duration - time_remaining) / duration;
            minutes = (int)(time_remaining / 60); 
            minutes = (int)(time_remaining % 60);

            // Format waktu dengan 2 digit angka
            if(timer_text) timer_text.text = $"{minutes:00}:{seconds:00}";

            yield return null;
        }

        // Pastikan nilai akhir benar-benar 1
        fill_image.fillAmount = 1f;
        if(timer_text) timer_text.text = "00:00";

        co_cooldown = null;
        yield break;
    }
    #endregion


    public void ReduceFill(float duration)
    {
        if (isCoolDown) return;

        if(co_reduceFill != null)
        {
            StopCoroutine(co_reduceFill);
            co_reduceFill = null;
        }

        co_reduceFill = StartCoroutine(OnReduceFill(duration));
    }

    public IEnumerator OnReduceFill(float duration)
    {
        float time_remaining = duration;
        fill_image.fillAmount = time_remaining / duration;

        while (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            fill_image.fillAmount = time_remaining / duration;
            yield return null;
        }

        // Pastikan nilai akhir benar-benar 0
        fill_image.fillAmount = 0f;

        co_reduceFill = null;
        yield break;
    }



}
