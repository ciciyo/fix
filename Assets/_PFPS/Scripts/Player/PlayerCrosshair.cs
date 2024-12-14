using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class PlayerCrosshair : MonoBehaviour
    {
        private Camera _camera;
        private Ray _ray;
        private RaycastHit _hitInfo;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _ray.origin = _camera.transform.position;
            _ray.direction = _camera.transform.forward;
            if (Physics.Raycast(_ray, out _hitInfo))
            {
                transform.position = _hitInfo.point;
            }
            else
            {
                transform.position = _ray.origin + _ray.direction * 25f;
            }
        }
    }
}
