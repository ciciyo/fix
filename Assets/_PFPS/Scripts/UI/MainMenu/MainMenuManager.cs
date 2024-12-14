using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlexusTechdev.Utility;
using PlexusTechdev.Utility.ScreenManagement;
using HeavenFalls;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameData gameData;
    public ScreenManager screenManager;
    public PopUpDisplay popUpDisplay;
    public ScreenLobby screenLobby;
    public MapSelectScreen mapSelectScreen;


    private void Awake()
    {
        instance = this;
    }

    public void OpenScene(string sceneName)
    {
        screenManager.OpenScreen(sceneName, true);
    }

    public void ShowNotif(string title, string message)
    {
        popUpDisplay.gameObject.SetActive(true);
        popUpDisplay.ShowPopUp(title, message);
    }

    public void SetCharacterSelected(Character character)
    {
        CharacterSelectedInfo info = new CharacterSelectedInfo();
        info.playerName = PlayerData.instance.profile.username;
        info.characterName = character.name;
        info.icon_characterType = gameData.characterTypeLibrary.GetCharacterTypeSprite(character.type);

        screenLobby.playerCharacterGUI.data = info;
        screenLobby.playerCharacterGUI.Initialize();

    }

    public void SetMission()
    {
        screenLobby.button_ready.SetActive(true);
        OpenLobbyScreen();
        screenManager.CloseScreen("map_select");
    }

    public void GoToGameplay()
    {
        if(GameManager.instance != null) GameManager.instance.GoToGameplay();
    }


    public void OpenLobbyScreen()
    {
        screenManager.OpenScreen("lobby", true);
    }

    public void OpenCustomizationScreen()
    {
        screenManager.OpenScreen("customization", true);
    }

    
    
}
