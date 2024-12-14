using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDraco : Ability
{
    
    [SerializeField] private float duration = 7f;
    [Range(0, 1)] public float improve_rate = 1f;
    
    [Header("aura")]
    public GameObject aura_prafab;
    private GameObject aura_active;
    public Transform aura_spawnPoint;
    protected override IEnumerator OnActivate()
    {
        var logAbility= $"{name}, \nAbility: {ability}";
        LogAbility.OnEventLog(logAbility);
        
        if (!m_player) yield break;
        
        m_player.SetImunity(false);
        if (m_player.weapon)
        {
            float damage = m_player.weapon.config.gunProperties.damage * (1 + improve_rate);
            m_player.weapon.config.gunProperties.damage = (int)damage;
        }
        if (aura_prafab)
        {
            if (aura_active != null) aura_active.SetActive(true);
            else
            {
                aura_active = Instantiate(aura_prafab, aura_spawnPoint);
            }
        }
        yield return new WaitForSeconds(duration);
        m_player.SetImunity(true);
        if (m_player.weapon)
        {
            float damage = m_player.weapon.config.gunProperties.damage;
            m_player.weapon.config.gunProperties.damage = (int)(damage / (1 + improve_rate));
        }
        if (aura_prafab)
        {
            if (aura_active != null) aura_active.SetActive(false);
        }

        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }
}
