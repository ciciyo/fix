using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls.Weapon
{
    [CreateAssetMenu(menuName = "Configuration/Weapon/Weapon configuration")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int m_level = 1;
        public WeaponStat stats;
        public List<WeaponUpgradeDetail> statUpgradeDetails;
        public int level => m_level;
        public bool isCanUpgrade => m_level < statUpgradeDetails.Count;


        //upgrade stat
        public void UpgradeStatByLevel(int level)
        {
            //jika sudah berada di level max abaikan
            if (level > statUpgradeDetails.Count) return;

            //hitung stat yang akan ditambahkan
            WeaponStat stat_toAdd = new WeaponStat();
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
        public WeaponStat GetPreviewUpgradeValue(int level)
        {
            //jika sudah di level max, berikan nilai stat saat ini
            if (level > statUpgradeDetails.Count) return stats;

            WeaponStat new_stats = stats;
            WeaponStat stat_toAdd = new WeaponStat();
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
        public WeaponStat GetPreviewStatNextLevel()
        {
            return GetPreviewUpgradeValue(m_level + 1);
        }

    }


    [System.Serializable]
    public struct WeaponUpgradeDetail
    {
        public int level;
        public WeaponStat stat;
    }
}