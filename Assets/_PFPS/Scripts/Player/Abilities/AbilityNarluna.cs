using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityNarluna : Ability
{
    [SerializeField] private float detectionRange = 40f;

    [Header("dummy")]
    public AudioSource audioSource_taunt;


    protected override IEnumerator OnActivate()
    {
        List<Enemy> enemies = GetEnemies();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i]._targetAttack = m_player.transform;
            enemies[i].defense /= 2;
            enemies[i].Invoke("ShowAlert", 1f);
        }
        if(audioSource_taunt) audioSource_taunt.Play();

        yield return new WaitForSeconds(3f);
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }

    private List<Enemy> GetEnemies()
    {
        // Cari semua objek dengan komponen Enemy dalam radius tertentu
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        int enemyCount = 0;
        List<Enemy> enemies = new List<Enemy>();

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyCount++;
                enemies.Add(enemy);
            }
        }
        //Debug.Log($"Jumlah Enemy yang ditemukan dalam radius {detectionRange}: {enemyCount}");
        return enemies;
        
    }

}
