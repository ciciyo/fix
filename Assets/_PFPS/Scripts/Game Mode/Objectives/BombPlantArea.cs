using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class BombPlantArea : MonoBehaviour
    {
        [SerializeField] private Bomb bombPrefab;
        
        [Header("Setup Area")]
        [SerializeField] private Vector3 boxCenter = Vector3.zero;
        [SerializeField] private Vector3 boxSize = new Vector3(1, 1, 1);
        [SerializeField] private Quaternion boxOrientation = Quaternion.identity;
        [SerializeField] private LayerMask layerMask;

        private float _holdTime;
        private const float HoldDuration = 2f;

        private const int MaxColliders = 4;
        private readonly Collider[] _colliders = new Collider[MaxColliders];

        private void Start()
        {
            BombCountDisplay.OnShow(false);
        }

        private void Update()
        {
            OnPlanting();
        }

        private void OnPlanting()
        {
            var numColliders = Physics.OverlapBoxNonAlloc(
                transform.position + boxCenter, 
                boxSize / 2, 
                _colliders, 
                boxOrientation, 
                layerMask
            );

            for (int i = 0; i < numColliders; i++)
            {
                if (Input.GetKey(KeyCode.F) && !GameModeInfiltrateBomb.IsBombActivate)
                {
                    BombCountDisplay.OnShow(true);

                    _holdTime += Time.deltaTime;
                    var count = _holdTime.ToString("F");
                    const string status = "Planting!";
                    
                    BombCountDisplay.OnCount(status, count);

                    if (_holdTime >= HoldDuration)
                    {
                        BombPlanted(i);
                    }
                }
                else if(!GameModeInfiltrateBomb.IsBombActivate)
                {
                    _holdTime = 0;
                    BombCountDisplay.OnShow(false);
                }
            }
        }

        private void BombPlanted(int i)
        {
            float yPos = 0;
            if (Physics.Raycast(_colliders[i].transform.position, Vector3.down, out var hit))
            {
                yPos = hit.point.y;
            }

            var bombPos = new Vector3(_colliders[i].transform.position.x, yPos, _colliders[i].transform.position.z);
            Instantiate(bombPrefab, bombPos, Quaternion.identity);
            GameModeInfiltrateBomb.IsBombActivate = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position + boxCenter, boxOrientation, boxSize);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
