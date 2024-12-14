using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    public class EscortObjective : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private List<Transform> positions = new();

        private Transform _firstPosition;
        private Transform _nextPosition;

        private const float Radius = 10;
        private LayerMask _playerLayer;
        
        private int _checkPoint;
        
        private void Start()
        {
            _playerLayer = LayerMask.GetMask("Player");

            _firstPosition = positions[_checkPoint];
            _checkPoint++;
            _nextPosition = positions[_checkPoint];
            
            var startPos = _firstPosition.transform.position;
            transform.position = new Vector3(startPos.x, transform.position.y, startPos.z);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _nextPosition.position) > 0.1f)
            {
                // transform.Translate(Vector3.forward * (speed * Time.deltaTime));

                if (IsCollideWithPlayer)
                {
                    transform.Translate(Vector3.forward * (speed * Time.deltaTime));
                }
                else
                {
                    var distance = Vector3.Distance(transform.position, _firstPosition.position);
                    if (distance > 2.1f)
                    {
                        transform.Translate(Vector3.forward * (-speed * Time.deltaTime));
                    }
                    else
                    {
                        var pos = new Vector3(_firstPosition.position.x, 1.75f, _firstPosition.position.z);
                        transform.position = pos;
                    }
                }
                transform.LookAt(_nextPosition);
            }
            else
            {
                var pos = new Vector3(_nextPosition.position.x, 1.75f, _nextPosition.position.z);
                transform.position = pos; // Snap to the end position
                
                _firstPosition = positions[_checkPoint];
                _checkPoint++;
                if (_checkPoint >= positions.Count)
                {
                    GameModeEscort.IsReachDestination = true;
                    return;
                }
                _nextPosition = positions[_checkPoint];
            }
        }

        private bool IsCollideWithPlayer
        { 
            get
            {
                var results = new Collider[5];
                var hit = Physics.OverlapSphereNonAlloc(transform.position, Radius, results, _playerLayer);
                
                return hit > 0;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}