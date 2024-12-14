using System;
using System.Collections;
using UnityEngine;

namespace HeavenFalls
{
    public class Bomb : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health = 100;
        [SerializeField] private float duration = 5f;

        [Space]
        [SerializeField] private GameObject modelBomb;
        [SerializeField] private ParticleSystem explosionVfx;

        private float _currentHealth;
        private float _countdown;
        
        private void Start()
        {
            _currentHealth = health;
            _countdown = duration;
        }

        private void OnDestroy()
        {
            BombCountDisplay.OnShow(false);
        }

        private void Update()
        {
            if (GameModeInfiltrateBomb.IsBombActivate) Countdown();
            print(_currentHealth);
        }

        private void Countdown()
        {
            _countdown -= Time.deltaTime;
            var count = _countdown.ToString("F");
            const string status = "Bomb Planted!";
            BombCountDisplay.OnCount(status, count);
                
            if (_countdown <= 0f)
            {
                BombCountDisplay.OnShow(false);
                Explode();
                GameModeInfiltrateBomb.IsBombActivate = false;
            }
        }
        
        private void Explode()
        {
            StartCoroutine(WaitTheExplosion());
        }

        private IEnumerator WaitTheExplosion()
        {
            explosionVfx.Play();
            GameModeInfiltrateBomb.OnEventBombExplode();
            modelBomb.SetActive(false);
            yield return new WaitUntil(() => explosionVfx.isStopped);
            Destroy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            
            if (_currentHealth <= 0)
            {
                print("bomb destroyed");
                GameModeInfiltrateBomb.OnEventBombDestroyed();
                if (GameplayGUIManager.instance != null) GameplayGUIManager.instance.EndGame(true);
                Destroy(gameObject);
            }
        }
    }
}