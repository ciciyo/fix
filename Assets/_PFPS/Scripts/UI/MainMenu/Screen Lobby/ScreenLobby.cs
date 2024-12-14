using HeavenFalls;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLobby : MonoBehaviour
{
    [Tooltip("object untuk loading wait for other player")]
    public GameObject waitOtherPlayer_GO;

    //[Header("buttons")]
    public GameObject button_ready;
    public GameObject button_cancel;
    public float countDownDuration = 15;
    public TMP_Text countDownText;
    public TMP_Text playerCountText;
    public int maxPlayer = 4;
    public int currentPlayerCount = 0;

    public PlayerCharacterGUI playerCharacterGUI;

    private Coroutine co_waitOtherPlayer;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        waitOtherPlayer_GO.SetActive(false);
        button_ready.SetActive(false);
        button_cancel.SetActive(true);
    }

    public void CancelWaitOtherPlayer()
    {
        if (co_waitOtherPlayer == null) return;
        else
        {
            StopCoroutine(co_waitOtherPlayer);
            co_waitOtherPlayer = null;
            waitOtherPlayer_GO.SetActive(false);
        }
    }

    public void WaitForOtherPlayer()
    {
        if (co_waitOtherPlayer != null) return;

        co_waitOtherPlayer = StartCoroutine(OnWaitForOtherPlayer());
        button_ready.SetActive(false);
    }

    private IEnumerator OnWaitForOtherPlayer()
    {
        waitOtherPlayer_GO.SetActive(true);
        float currentTime = countDownDuration;
        countDownText.text = $"{(int)currentTime}";
        SetPlayerCount(0, maxPlayer);
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;
            countDownText.text = $"{(int)currentTime}";
        }
        button_cancel.SetActive(false);
        SetPlayerCount(maxPlayer, maxPlayer);
        yield return new WaitForSeconds(1f);
        MainMenuManager.instance.GoToGameplay();
        co_waitOtherPlayer = null;
        yield break;
    }


    public void SetPlayerCount(int value, int maxPlayer)
    {
        currentPlayerCount = value;
        playerCountText.text = $"{value} out of {maxPlayer} players";
    }

}
