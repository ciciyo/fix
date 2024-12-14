using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayGUIManager : MonoBehaviour
{
    public static GameplayGUIManager instance;

    public ResultScreen resultScreen;

    private void Awake()
    {
        instance = this;
    }

    

    public void EndGame(bool isWin)
    {
        resultScreen.ShowResultScreen(isWin);
        resultScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }


    public void GoToMainMenu()
    {
        if (GameManager.instance != null) GameManager.instance.GoToMainMenu();
    }
}
