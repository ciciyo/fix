using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HeavenFalls
{
    [CreateAssetMenu(menuName = "Configuration/Character Configuration")]
    public class CharacterConfig : ScriptableObject
    {
        public int m_level = 1;
        
        [SerializeField] private ConfigStats stats;
        [SerializeField] private ConfigMovement move;
        [SerializeField] private ConfigJump jump;
        [SerializeField] private ConfigAim aim;
        [SerializeField] private float height;
        [SerializeField] private float radius;
        [SerializeField] private List<StatUpgradeDetail> statUpgradeDetails;


        [Header("TESTING")]
        [SerializeField] private int m_levelToUpgrade = 2;

        public ConfigStats Stats => stats;
        public ConfigMovement Movement => move;
        public ConfigJump Jump => jump;
        public ConfigAim Aim => aim;
        public int level => m_level;
        public bool isCanUpgrade => m_level < statUpgradeDetails.Count;


        public (float height, float radius) GetSetup => (height, radius);


        

        //upgrade stat
        public void UpgradeStatByLevel(int level)
        {
            //jika sudah berada di level max abaikan
            if (level > statUpgradeDetails.Count) return;

            //hitung stat yang akan ditambahkan
            ConfigStats stat_toAdd = new ConfigStats();
            for (int i = m_level; i < level; i++)
            {
                stat_toAdd.AddStat(statUpgradeDetails[m_level].stat);
                m_level++;
            }

            //update nilai stat
            stats.AddStat(stat_toAdd);
        }

        [ContextMenu("Upgrade Next Level")]
        public void UpgradeStatNextLevel()
        {
            UpgradeStatByLevel(m_level + 1);
        }


        /// <summary>
        /// Memperlihatkan perkiraan perbandingan stat pemain saat ini dengan level selanjutnya jika ia melakukan upgrade
        /// digunakan saat preview upgrade level
        /// </summary>
        /// <param name="level"></param>
        public ConfigStats GetPreviewUpgradeValue(int level)
        {
            //jika sudah di level max, berikan nilai stat saat ini
            if (level > statUpgradeDetails.Count) return this.stats;

            ConfigStats new_stats = this.Stats;
            ConfigStats stat_toAdd = new ConfigStats();
            for (int i = m_level; i < level; i++)
            {
                stat_toAdd.AddStat(statUpgradeDetails[m_level].stat);
            }
            new_stats.AddStat(stat_toAdd);
            return new_stats;
        }

        /// <summary>
        /// Cek preview untuk level selanjutnya
        /// </summary>
        /// <returns></returns>
        public ConfigStats GetPreviewStatNextLevel()
        {
            return GetPreviewUpgradeValue(m_level + 1);
        }


        /// <summary>
        /// Testing upgrade
        /// silahkan hapus jika sudah tidak diperlukan. hapus juga field m_levelToUpgrade
        /// </summary>
        [ContextMenu("Upgrade Level To")]
        public void UpgradeStatLevelTest()
        {
            UpgradeStatByLevel(m_levelToUpgrade);
        }
    }



    [System.Serializable]
    public struct StatUpgradeDetail
    {
        public int level;
        public ConfigStats stat;
    }
}

