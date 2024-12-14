using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HeavenFalls
{
    public class GameModeInfiltrateBomb : MonoBehaviour
    {
        public static bool IsBombActivate;
        
        [SerializeField] private Enemy prefabEnemy;

        [SerializeField] private float enemySpawnInterval;
        [SerializeField] private List<Transform> spawnPoints = new();
        
        private ObjectPooling<Enemy> _poolEnemy;
        private readonly List<Enemy> _enemies = new();
        
        private bool _isGameStopped = false;

        public static event Action EventBombDestroyed, EventBombExplode;

        private void Awake()
        {
            _poolEnemy = new ObjectPooling<Enemy>(prefabEnemy);
            
            if (spawnPoints.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    spawnPoints.Add(child);
                }
            }   
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 1, enemySpawnInterval);
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }


        private void SubscribeEvent()
        {
            Player.OnDeath += GameOver;
            EventBombDestroyed += Finish;
            EventBombExplode += Finish;
        }

        private void UnsubscribeEvent()
        {
            Player.OnDeath -= GameOver;
            EventBombDestroyed -= Finish;
            EventBombExplode -= Finish;
        }

        private void GameOver()
        {
            CancelInvoke();
            foreach (var enemy in _enemies.Where(enemy => enemy.isActiveAndEnabled))
            {
                _poolEnemy.ReleaseObject(enemy);
            }
            if (GameplayGUIManager.instance != null) GameplayGUIManager.instance.EndGame(false);
        }
        
        private void SpawnEnemy()
        {
            var enemy = _poolEnemy.GetObject;
            _enemies.Add(enemy);
            
            // spawn at random point.
            var spawnPoint = Random.Range(0, spawnPoints.Count);
            enemy.transform.position = spawnPoints[spawnPoint].position;
            
            enemy.OnDeath += () =>
            {
                _poolEnemy.ReleaseObject(enemy);
            };
        }

        private void Finish()
        {
            // Stop game mode.
            CancelInvoke();
            
            foreach (var enemy in _enemies.Where(enemy => enemy.isActiveAndEnabled))
            {
                _poolEnemy.ReleaseObject(enemy);
            }
            
            _isGameStopped = true;
            if (GameplayGUIManager.instance != null) GameplayGUIManager.instance.EndGame(false);
        }

        public static void OnEventBombDestroyed()
        {
            EventBombDestroyed?.Invoke();
        }

        public static void OnEventBombExplode()
        {
            EventBombExplode?.Invoke();
        }
    }
}
