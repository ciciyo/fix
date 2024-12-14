using HeavenFalls;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{

    private List<IDamageable> enemies;
    public float damage = 5f;
    [SerializeField] private float cooldown = 0.1f;
    [SerializeField] private float current_time;

    private RaycastHit _hitInfo;
    private Ray _ray;
    public Transform shootPoint;
    public Transform destinationPoint;
    [SerializeField] private LayerMask hit_layer;
    public float range = 25f;
    public TrailRenderer trailRenderer;
    public ParticleSystem hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        destinationPoint.localPosition = shootPoint.localPosition + new Vector3(0, 0, range);
    }

    // Update is called once per frame
    void Update()
    {
        // Mendapatkan posisi mouse di layar
        Vector3 mousePosition = Input.mousePosition;

        // Membuat ray dari kamera menuju posisi mouse
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        // Periksa apakah ray bertabrakan dengan objek di dunia
        if (Physics.Raycast(mouseRay, out RaycastHit mouseHit, Mathf.Infinity, hit_layer))
        {
            // Menentukan posisi destinationPoint berdasarkan lokasi tabrakan
            destinationPoint.position = mouseHit.point;
        }
        else
        {
            // Jika tidak ada tabrakan, default posisi jauh ke depan
            destinationPoint.position = mouseRay.GetPoint(range);
        }


        var shootOrigin = shootPoint.position;
        _ray.origin = shootOrigin;
        _ray.direction = destinationPoint.position - shootOrigin;

        if (Physics.Raycast(_ray, out _hitInfo, range, hit_layer))
        {
            
            var hitEffectTransform = hitEffect.transform;
            hitEffectTransform.position = _hitInfo.point;
            hitEffectTransform.forward = _hitInfo.normal;
            hitEffect.gameObject.SetActive(true);
            destinationPoint.position = _hitInfo.point;
            if (_hitInfo.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                if (current_time > cooldown)
                {
                    damageable.TakeDamage(damage);
                    current_time = 0f;
                }
                //print(_hitInfo.transform.name);
                
            }
        }
        else
        {
            hitEffect.gameObject.SetActive(false);
        }

        current_time += Time.deltaTime;
        /*if (current_time > cooldown)
        {
            DamageToAllEnemy();
            current_time = 0f;
        }*/
    }


    public void DamageToAllEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log("enemy get damage"); 
            if (enemies[i] != null) enemies[i].TakeDamage(damage);
        }
    }

    private void OnEnable()
    {
        enemies = new List<IDamageable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("enemy enter");
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }
}
