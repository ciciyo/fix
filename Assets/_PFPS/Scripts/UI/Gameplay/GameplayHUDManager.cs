using HeavenFalls.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace HeavenFalls
{
    public class GameplayHUDManager : MonoBehaviour
    {
        public static GameplayHUDManager instance;


        public Player player; 

        [Header("timer")]
        [SerializeField] private float playTime;
        public TMP_Text playTime_text;


        [Header("Health")]
        public Image health_slider;
        public Image shield_slider;
        public Volume postProcessing;
        public Vignette vignette_criticalHealth;

        [Header("Weapon")]
        public TMP_Text currentAmmoText;
        public TMP_Text totalAmmoText;
        public Color ammo_normalColor;
        public Color ammo_minColor;
        public int trigger_minColor = 5;

        [Header("reload")]
        public GameObject container_reload;
        public Slider slider_reload;


        

        //coroutine
        private Coroutine co_healthBar;
        private Coroutine co_shieldBar;
        private Coroutine co_vignette;
        
        
        



        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdateHealthBar(1);
            UpdateShieldBar(1);

            
            

            playTime = 0f;


            if (postProcessing.profile.TryGet< Vignette>(out Vignette vig))
            {
                vignette_criticalHealth = vig;
            }
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void OnDestroy()
        {
            UnSubscribeEvent();
        }



        private void Update()
        {
            playTime += Time.deltaTime;
            UpdatePlayTime(playTime);

        }


        public void ShowReloadPanel(float duration)
        {
            StartCoroutine(OnShowReloadPanel(duration));
        }

        private IEnumerator OnShowReloadPanel(float duration)
        {
            float currentTime = 0;
            container_reload.SetActive(true);
            slider_reload.value = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                slider_reload.value = currentTime/duration;
                yield return null;
            }
            container_reload.SetActive(false);
            yield break;
        }

        private void UpdatePlayTime(float seconds)
        {
            int totalSeconds = Mathf.FloorToInt(seconds);
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int secs = totalSeconds % 60;

            playTime_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, secs);
        }


        private void SubscribeEvent()
        {
            if(player != null)
            {
                player.onUpdateHealth += UpdateHealthBar;
                player.onUpdateShield += UpdateShieldBar;

                if (player.weapon != null)
                {
                    player.weapon.onFire += UpdateAmmoText;
                    player.weapon.onReload += ShowReloadPanel;
                    player.weapon.onReloadFinish += UpdateAmmoText;
                }
            }
        }

        public void UpdateAmmoText(int current_ammo, int total_ammo)
        {
            currentAmmoText.text = $"{current_ammo}";
            currentAmmoText.color = current_ammo <= trigger_minColor? ammo_minColor: ammo_normalColor;
            totalAmmoText.text = $"{total_ammo}";
        }


        private void UnSubscribeEvent()
        {
            if (player != null)
            {
                player.onUpdateHealth -= UpdateHealthBar;
                if (player.weapon != null)
                {
                    player.weapon.onFire -= UpdateAmmoText;
                    player.weapon.onReload -= ShowReloadPanel;
                    player.weapon.onReloadFinish -= UpdateAmmoText;
                }
            }
        }


        //merubah fill image dengan smooth.
        private IEnumerator OnUpdateBarByImageFill(float value, Image bar)
        {
            float slider_baseValue = bar.fillAmount;

            //set durasi 
            float duration = 0.1f;

            float currentTime = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                bar.fillAmount = Mathf.Lerp(slider_baseValue, value, currentTime / duration);
                yield return null;
            }

            yield break;
        }

        #region health, shield

        [Tooltip("value between 0,1")]
        public void UpdateHealthBar(float value)
        {
            //Debug.Log("update health bar");
            if (co_healthBar != null)
            {
                StopCoroutine(co_healthBar);
                co_healthBar = null;
            }
            co_healthBar = StartCoroutine(OnUpdateBarByImageFill(value, health_slider));

            if (vignette_criticalHealth != null)
            {
                if(value > (0.3f))
                {
                    //jika menyala
                    if (vignette_criticalHealth.intensity.value > 0)
                    {
                        DeactivateVignatte();
                    }
                }
                else
                {
                    //jika mati
                    if (vignette_criticalHealth.intensity.value <= 0)
                    {
                        ActivateVignette();
                    }
                }
            }
            //health_slider.fillAmount = value;
        }

        public void ActivateVignette()
        {
            if(co_vignette != null)
            {
                StopCoroutine(co_vignette);
                co_vignette = null;
            }

            co_vignette = StartCoroutine(OnSetVignette(0.5f));
        }

        public void DeactivateVignatte()
        {
            if (co_vignette != null)
            {
                StopCoroutine(co_vignette);
                co_vignette = null;
            }

            co_vignette = StartCoroutine(OnSetVignette(0f));
        }

        public IEnumerator OnSetVignette(float intensity)
        {
            float duration = 0.15f;
            float currentTime = 0f;
            float base_value = vignette_criticalHealth.intensity.value;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                vignette_criticalHealth.intensity.value = Mathf.Lerp(base_value, intensity, currentTime / duration);
                yield return null;
            }


        }




        [Tooltip("value between 0,1")]
        public void UpdateShieldBar(float value)
        {
            if (co_shieldBar != null)
            {
                StopCoroutine(co_shieldBar);
                co_shieldBar = null;
            }
            co_shieldBar = StartCoroutine(OnUpdateBarByImageFill(value, shield_slider));
        }

        #endregion


        #region Weapon




        

        #endregion
    }
}