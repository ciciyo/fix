using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityKallos : Ability
{
    public float duration = 7f;
    public GameObject aura_prafab;
    private GameObject aura_active;
    public Transform aura_spawnPoint;

    protected override IEnumerator OnActivate()
    {
        var logAbility= $"{name}, \nAbility: {ability}";
        LogAbility.OnEventLog(logAbility);
        
        if (!m_player) yield break;
        controlView.ReduceFill(duration);
        int damage = m_player.weapon.config.gunProperties.damage;
        float moveSpeed = m_player.speed_run;
        m_player.speed_run = moveSpeed * 2;
        m_player.defense = m_player.defense / 2;
        if (m_player.weapon)
        {
            m_player.weapon.config.gunProperties.damage = damage * 2;
            
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
        if (aura_prafab)
        {
            if (aura_active != null) aura_active.SetActive(false);
        }
        m_player.speed_run = moveSpeed;
        m_player.defense = m_player.defense_max;
        if (m_player.weapon)
        {
            m_player.weapon.config.gunProperties.damage = damage;
        }
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }
}
