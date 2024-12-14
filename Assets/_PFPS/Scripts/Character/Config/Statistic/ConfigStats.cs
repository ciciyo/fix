using System;

namespace HeavenFalls
{
    [Serializable]
    public struct ConfigStats
    {
        public float health;
        public float stamina;
        public float damage;
        public float defense;
        public float shield;
        public float speed;
        public float skills;


        public void AddStat(ConfigStats stat)
        {
            health += stat.health;
            stamina += stat.stamina;
            damage += stat.damage;
            defense += stat.defense;
            shield += stat.shield;
            speed += stat.speed;
            skills += stat.skills;

        }


        public void SetStat(ConfigStats stat)
        {
            health = stat.health;
            stamina = stat.stamina;
            damage = stat.damage;
            shield = stat.shield;
            defense = stat.defense;
            speed = stat.speed;
            skills = stat.skills;
        }

    }
}