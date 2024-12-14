using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class AbilityAjax : Ability
    {
        [Space]
        public float duration = 7f;
        [Range(0,1)] public float improve_rate = 0.7f;

        [Header("vfx")]
        public Material[] forceField_mats;
        public List<SkinnedMeshRenderer> skinneds;
        public Dictionary<SkinnedMeshRenderer, Material[]> dictionary_materialByMesh;


        private void Start()
        {
            dictionary_materialByMesh = new Dictionary<SkinnedMeshRenderer, Material[]>();

            for (int i = 0; i < skinneds.Count; i++)
            {
                dictionary_materialByMesh.Add(skinneds[i], skinneds[i].materials);
            } 
        }

        public void ChangeSkinToForceField()
        {
            for (int i = 0; i < skinneds.Count; i++)
            {
                skinneds[i].materials = forceField_mats;
            }
        }

        public void RestoreSkin()
        {
            for (int i = 0; i < skinneds.Count; i++)
            {
                skinneds[i].materials = dictionary_materialByMesh[skinneds[i]];
            }
        }

        protected override IEnumerator OnActivate()
        {
            var logAbility= $"{name}, \nAbility: {ability}";
            LogAbility.OnEventLog(logAbility);
            
            Enemy[] enemies = GetEnemies();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i]._targetAttack == m_player.transform)
                {
                    enemies[i]._targetAttack = null;
                }
            }
            //update damage power
            if (m_player.weapon)
            {
                float damage = m_player.weapon.config.gunProperties.damage * (1 + improve_rate);
                m_player.weapon.config.gunProperties.damage = (int)damage;
            }
            ChangeSkinToForceField();

            float current_time = duration;
            bool isPlayerAttacking = false;
            while (!isPlayerAttacking && current_time > 0)
            {
                isPlayerAttacking = m_player.isShooting;
                if (isPlayerAttacking) current_time = 0f;
                current_time -= Time.deltaTime;
                yield return null;
            }

            if (m_player.weapon)
            {
                float damage = m_player.weapon.config.gunProperties.damage;
                m_player.weapon.config.gunProperties.damage = (int)(damage / (1 + improve_rate));
            }
            RestoreSkin();
            controlView.CoolDown(cooldown_time);
            co_activate = null;
            yield break;
        }

        private Enemy[] GetEnemies()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();


            return enemies;

        }
    }
}