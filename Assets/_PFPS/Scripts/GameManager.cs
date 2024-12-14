using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int characterId_selected = 1;
    public int weaponId_selected = 1;
    public Mission mission_selected;
    public SO_CharacterData so_characterData;

    [Header("Scene Management")]
    [SerializeField] private string sceneName_mainMenu;
    [SerializeField] private string sceneName_Gameplay;
    [SerializeField] private string sceneName_UIGameplay;
    [SerializeField] private string sceneName_map_captureFlag;
    [SerializeField] private string sceneName_map_destroyComms;
    [SerializeField] private string sceneName_map_escort;
    [SerializeField] private string sceneName_map_rampage;
    [SerializeField] private string sceneName_map_infiltrateBomb;
    [SerializeField] private string sceneName_map_destroyItem;
    [SerializeField] private string sceneName_map_rescueHostage;

    public GameObject canvas_loadScene;
    public TMP_Text loading_text;
    public Slider progressBar_loadScene;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    

    public void GoToGameplay()
    {
        StartCoroutine(OnLoadGameplay());
    }

    public IEnumerator OnLoadGameplay()
    {
        Time.timeScale = 0f;
        loading_text.text = "Loading...";
        yield return LoadSceneCoroutine(sceneName_Gameplay);
        loading_text.text = "Initialize Map...";
        string sceneMapName = GetMapNameByMissionType(mission_selected.missionType);
        yield return LoadSceneAdditive(sceneMapName);
        loading_text.text = "Initialize UI...";
        yield return LoadSceneAdditive(sceneName_UIGameplay);
        Time.timeScale = 1f;
        var sceneMap = SceneManager.GetSceneByName(sceneMapName);
        SceneManager.SetActiveScene(sceneMap);
        yield break;
    }


    private string GetMapNameByMissionType(MissionType type)
    {
        switch (type)
        {
            case MissionType.KillTheBoss:
                //harus diubah
                return sceneName_map_destroyItem;
            case MissionType.DestroyItem:
                return sceneName_map_destroyItem;
            case MissionType.Rampage:
                return sceneName_map_rampage;
            case MissionType.CaptureTheFlag:
                return sceneName_map_captureFlag;
            case MissionType.Escort:
                return sceneName_map_escort;
            case MissionType.DestroyCommTower:
                return sceneName_map_destroyComms;
            case MissionType.PlaceTheBomb:
                return sceneName_map_infiltrateBomb;
            case MissionType.RescueHostage:
                return sceneName_map_rescueHostage;
            default:
                break;
        }

        return sceneName_map_rampage;
    }
    


    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // Coroutine untuk meng-handle loading scene secara asinkron
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Jangan aktifkan scene langsung setelah loading selesai
        canvas_loadScene.SetActive(true);
        // Selama scene masih dalam proses loading
        while (!asyncLoad.isDone)
        {
            // Update progres ke progress bar
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar_loadScene.value = progress;

            // Periksa jika loading selesai (progress mencapai 0.9)
            if (asyncLoad.progress >= 0.9f)
            {
                // Aktifkan scene setelah loading selesai dan pengguna sudah siap
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
        canvas_loadScene.SetActive(false);
    }



    private IEnumerator LoadSceneAdditive(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false; // Jangan aktifkan scene langsung setelah loading selesai
        canvas_loadScene.SetActive(true);
        // Selama scene masih dalam proses loading
        while (!asyncLoad.isDone)
        {
            // Update progres ke progress bar
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar_loadScene.value = progress;

            // Periksa jika loading selesai (progress mencapai 0.9)
            if (asyncLoad.progress >= 0.9f)
            {
                // Aktifkan scene setelah loading selesai dan pengguna sudah siap
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
        canvas_loadScene.SetActive(false);
    }

    internal void GoToMainMenu()
    {
        StartCoroutine(OnGoToMainMenu());
    }

    private IEnumerator OnGoToMainMenu()
    {
        loading_text.text = "Loading...";
        yield return LoadSceneCoroutine(sceneName_mainMenu); 
        yield break;
    }
}
