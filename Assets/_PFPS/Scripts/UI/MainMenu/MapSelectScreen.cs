using Cinemachine;
using HeavenFalls.UI;
using PlexusTechdev;
using PlexusTechdev.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectScreen : MonoBehaviour
{

    public GameObject camera_map;
    public GameObject camera_default;

    [Header("Map")]
    public SO_Mission so_mission;
    public TMP_Text map_nameText;
    public TMP_Text map_progressText;
    public Slider map_sliderProgress;
    public TabManager tabManager_footer;
    public MapSlot mapSlot_prefab;
    public Transform mapSlot_parent;
    public List<Transform> map_slotPoints;
    public List<MapSlot> map_slots;

    [Header("Mission")]
    public GameObject panel_mission;
    public MissionSlot missionSlot_prefab;
    public Transform missionSlot_parent;
    public List<MissionSlot> mission_slots;
    public TMP_Text mission_nameText;
    public TMP_Text mission_nameLockedText;
    public ImageFillGroup mission_difficultyControlView;
    public TMP_Text mission_descriptionText;
    public RewardControlView rewardControlView;
    public GameObject mission_containerPlay;
    public GameObject mission_containerPlayAgain;
    public GameObject mission_containerComplete;
    public GameObject container_missionDetailOpen;
    public GameObject container_missionDetailLocked;


    [SerializeField] private Map map_selected;
    private MapSlot slot_selected;
    [SerializeField] private Mission mission_selected;
    private MissionSlot missionSlot_selected;




    private void OnEnable()
    {
        camera_map.gameObject.SetActive(true);
        camera_default.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        camera_map.gameObject.SetActive(false);
        camera_default.gameObject.SetActive(true);
    }

    #region Map
    public void InitializeMap()
    {
        if (so_mission == null) return;

        ResetDataMap();

        

        for (int i = 0; i < so_mission.maps.Count; i++)
        {
            if (map_slots.Count > i)
            {
                map_slots[i].data = so_mission.maps[i];
                map_slots[i].Initialize();
                map_slots[i].gameObject.SetActive(true);
            }
            else
            {
                MapSlot slot = Instantiate(mapSlot_prefab, mapSlot_parent);
                slot.data = so_mission.maps[i];
                if(slot.TryGetComponent(out Button_tab tab_button))
                {
                    tab_button.tabManager = tabManager_footer;
                    tab_button.id = i;
                    tab_button.UpdateCustomUI(i == 0);  
                }

                
                slot.Initialize();
                slot.onSelect += SetMapSelected;
                map_slots.Add(slot);
                if(i == 0)
                {
                    slot.Select();
                }
            }
        }
    }

    public void SetMapSelected(int id)
    {
       
        
        map_selected = so_mission.GetMapById(id);
        map_nameText.text = map_selected.name;
        map_sliderProgress.value = map_selected.GetProgressValue();
        //atur persentase
        int percentage = Mathf.RoundToInt(map_sliderProgress.value * 100);
        map_progressText.text = $"{percentage}%";
        //panel_mission.SetActive(true);
        InitializeMission();
    }

    private void ResetDataMap()
    {
        for (int i = 0; i < map_slots.Count; i++)
        {
            map_slots[i].gameObject.SetActive(false);
        }
    }
    #endregion

    #region Mission
    public void InitializeMission()
    {
        if (map_selected.id == 0) return;
        ResetDataMission();

        // batasi jumlahnya untuk mencegah munculnya error saat jumlah posisi nya < jumlah misi di scriptable
        int count = Mathf.Min(map_selected.missions.Count, map_slotPoints.Count);


        for (int i = 0; i < count; i++)
        {
            if (mission_slots.Count > i)
            {
                mission_slots[i].data = map_selected.missions[i];
                mission_slots[i].Initialize();
                mission_slots[i].gameObject.SetActive(true);
            }
            else
            {
                MissionSlot slot = Instantiate(missionSlot_prefab, missionSlot_parent);
                slot.transform.position = map_slotPoints[i].position;
                slot.data = map_selected.missions[i];
                slot.Initialize();
                slot.onSelect += PreviewMissionSelected;
                mission_slots.Add(slot);
            }

            mission_slots[0].UpdateHighlight(i == 0);
        }

        PreviewMissionSelected(1);
    }

    public void PreviewMissionSelected(int id)
    {
        if (missionSlot_selected != null) missionSlot_selected.UpdateHighlight(false);
        missionSlot_selected = mission_slots.Find(mission => mission.data.id == id);
        if (missionSlot_selected != null) missionSlot_selected.UpdateHighlight(true);

        mission_selected = map_selected.GetMissionByID(id);
        mission_nameText.text = mission_selected.name;
        mission_difficultyControlView.Initialize(mission_selected.difficulty);
        mission_descriptionText.text = mission_selected.description;
        rewardControlView.rewards = mission_selected.rewards;
        rewardControlView.Initialize();

        if (mission_selected.isLocked)
        {
            container_missionDetailLocked.SetActive(true);
            container_missionDetailOpen.SetActive(false);
            mission_nameLockedText.text = mission_selected.name;
        }
        else
        {
            container_missionDetailLocked.SetActive(false);
            container_missionDetailOpen.SetActive(true);

            if (mission_selected.isComplete)
            {
                mission_containerPlay.SetActive(false);
                mission_containerComplete.SetActive(true);
                mission_containerPlayAgain.SetActive(true);
            }
            else
            {
                mission_containerPlay.SetActive(true);
                mission_containerComplete.SetActive(false);
                mission_containerPlayAgain.SetActive(false);
            }
        }
        //update mission selected di game manager
        if (GameManager.instance != null) GameManager.instance.mission_selected = mission_selected;
    }

    public void InitializeMissionSelected()
    {
        
        MainMenuManager.instance.SetMission();
        
    }


    private void ResetDataMission()
    {
        for (int i = 0; i < mission_slots.Count; i++)
        {
            mission_slots[i].gameObject.SetActive(false);
        }
    }
    #endregion

}
