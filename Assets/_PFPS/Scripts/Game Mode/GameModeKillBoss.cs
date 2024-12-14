using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace HeavenFalls
{
    public class GameModeKillBoss : MonoBehaviour
    {
        [SerializeField] private Enemy prefabEnemy;
        [SerializeField] private Enemy prefabBossEnemy;
        [SerializeField] private Transform bossSpawn;

        [Space]
        [SerializeField] private float timeCounterMode;
        [SerializeField] private float enemySpawnInterval;
        [SerializeField] private List<Transform> spawnPoints = new();

        private Enemy _bossEnemy;
        private ObjectPooling<Enemy> _poolEnemy;
        private readonly List<Enemy> _enemies = new();
        
        private bool _isGameStopped = false;

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
            _bossEnemy = Instantiate(prefabBossEnemy, bossSpawn.position,Quaternion.identity);
            InvokeRepeating(nameof(SpawnEnemy), 1, enemySpawnInterval);
            SubscribeEvent();
        }

        private void OnDestroy()
        {
            UnsubscribeEvent();
        }

        private void SubscribeEvent()
        {
            Player.OnDeath += GameOver;
            _bossEnemy.OnDeath += PrefabBossDeath;
        }

        private void UnsubscribeEvent()
        {
            Player.OnDeath -= GameOver;
            _bossEnemy.OnDeath -= PrefabBossDeath;
        }

        private void PrefabBossDeath()
        {
            _bossEnemy.gameObject.SetActive(false);
            GameOver();
        }

        private void GameOver()
        {
            CancelInvoke();
            foreach (var enemy in _enemies.Where(enemy => enemy.isActiveAndEnabled))
            {
                _poolEnemy.ReleaseObject(enemy);
            }
            Timer.StopAndResetTimer(ref timeCounterMode, 60);
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

        private void Update()
        {
            if (!_isGameStopped)
            {
                var currentTime = Timer.CountdownTimer(ref timeCounterMode, Time.deltaTime);
                var timeMinutesAndSeconds = Timer.ConvertToMinutesAndSeconds(currentTime);

                const string mission = "Kill the boss";
                GameModeUI.OnUpdate(timeMinutesAndSeconds, mission);
            }
            
            if (!_bossEnemy.gameObject.activeSelf && !_isGameStopped)
            {
                // Stop game mode.
                CancelInvoke();
                
                foreach (var enemy in _enemies.Where(enemy => enemy.isActiveAndEnabled))
                {
                    _poolEnemy.ReleaseObject(enemy);
                }
                
                _isGameStopped = true;

                if (GameplayGUIManager.instance != null) GameplayGUIManager.instance.EndGame(true);
            }
        }
    }
}
