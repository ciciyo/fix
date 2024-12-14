using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour
{
    [SerializeField] protected Player m_player;
    [SerializeField] protected float cooldown_time = 5f;


    public string name;
    public string ability;
    
    [Header("ControlView")]
    public CoolDownControlView controlView;
    


    //coroutine
    private Coroutine co_cooldown;
    protected Coroutine co_activate;

    //property
    public bool isCoolDown => controlView.isCoolDown;

    [Tooltip("apakah sedang mengaktifkan ability?")]
    public bool isActivateAbility => co_activate != null;


    #region cooldown
    public void CoolDown()
    {
        if (isCoolDown)
        {
            StopCoroutine(co_cooldown);
            co_cooldown = null;
        }

        co_cooldown = StartCoroutine(OnCoolDown());
    }

    private IEnumerator OnCoolDown()
    {
        
        yield  return new WaitForSeconds(cooldown_time);
        co_cooldown = null;
        yield break;
    }


    #endregion


    #region Activate

    public void Activate()
    {
        if (isCoolDown || isActivateAbility) return;

        co_activate = StartCoroutine(OnActivate());
    }


    protected virtual IEnumerator OnActivate()
    {
        //lakukan ability
        Debug.Log("play ability");
        //aktifkan cooldown
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }

    #endregion

}
