using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HeavenFalls
{
    public class GameModeRescueHostage : MonoBehaviour
    {
        [SerializeField] private Enemy prefabEnemy;
        [SerializeField] private List<Transform> spawnPointsEnemy = new();
        private ObjectPooling<Enemy> _poolEnemy;
        private readonly List<Enemy> _enemies = new();

        [Space] 
        [SerializeField] private float timeCounterMode;
        [SerializeField] private float enemySpawnInterval;
        
        private bool _isGameStopped = false;
        private int _targetObjective;
        private static int objectiveCount;

        public static void CountHostage()
        {
            objectiveCount++;
        }
        
        private void Awake()
        {
            _poolEnemy = new ObjectPooling<Enemy>(prefabEnemy);
            var hostages = FindObjectsByType<Hostage>(FindObjectsSortMode.None);
            _targetObjective = hostages.Length;
            print(_targetObjective);
            
            if (spawnPointsEnemy.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    spawnPointsEnemy.Add(child);
                }
            }
        }
        
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }
        
        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 1, enemySpawnInterval);
        }
        
        private void SubscribeEvent()
        {
            Player.OnDeath += GameOver;
            // OnFlagReturned += FlagReturned;
            // OnFlagCaptured += FlagCaptured;
        }

        private void UnsubscribeEvent()
        {
            Player.OnDeath -= GameOver;
            // OnFlagReturned -= FlagReturned;
            // OnFlagCaptured -= FlagCaptured;
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
            var spawnPoint = Random.Range(0, spawnPointsEnemy.Count);
            enemy.transform.position = spawnPointsEnemy[spawnPoint].position;
            
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

                var killDescription = $"Rescue Hostage ({objectiveCount} / {_targetObjective})";
                GameModeUI.OnUpdate(timeMinutesAndSeconds, killDescription);
            }
            
            if (objectiveCount >= _targetObjective && !_isGameStopped)
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

            // if (isHasFlag)
            // {
            //     flagArea.SetActive(true);
            // }
            // else
            // {
            //     flagArea.SetActive(false);
            // }
        }

        private void FlagReturned()
        {
            // isHasFlag = false;

            objectiveCount++;

            if (objectiveCount >= _targetObjective)
            {
                CancelInvoke();
                foreach (var enemy in _enemies.Where(enemy => enemy.isActiveAndEnabled))
                {
                    _poolEnemy.ReleaseObject(enemy);
                }
                Timer.StopAndResetTimer(ref timeCounterMode, 60);
            }
        }

        private void FlagCaptured()
        {
            // isHasFlag = true;
        }
    }
}
