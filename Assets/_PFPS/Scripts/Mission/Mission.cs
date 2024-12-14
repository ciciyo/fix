using HeavenFalls.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum MissionType
{
    KillTheBoss, DestroyItem, Rampage, CaptureTheFlag, Escort, DestroyCommTower, PlaceTheBomb, RescueHostage
}

[System.Serializable]
public class Mission
{
    public int id;
    public string name;
    [TextArea(3, 5)]
    public string description;
    int scene_id;
    public MissionType missionType;
    [Range(0, 5)] public float difficulty;

    [Tooltip("apakah misi ini masih terkunci?")]
    public bool isLocked = true;
    [Tooltip("apakah misi ini sudah pernah dilewati dan sukses?")]
    public bool isComplete = true;
    public List<InventoryItem> rewards;

    public Mission GetClone()
    {
        Mission clone = new Mission();
        clone.id = id;
        clone.name = name;
        clone.description = description;
        clone.scene_id = scene_id;
        clone.missionType = missionType;
        clone.difficulty = difficulty;
        clone.isLocked = isLocked;
        clone.isComplete = isComplete;
        clone.rewards = rewards;
        return clone;
    }
}

[System.Serializable]
public struct Map
{
    public int id;
    public string name;
    [TextArea(3, 5)]
    public string description;

    public List<Mission> missions;


    public Mission GetMissionByID(int id) => missions[id-1];

    public float GetProgressValue()
    {
        float missionCompleteCount = missions.Where(mission => mission.isComplete == true).Count();
        return missionCompleteCount/missions.Count;
    }

    public Map GetClone()
    {
        Map clone = new Map();
        clone.id = id;  
        clone.name = name;
        clone.description = description;
        clone.missions = new List<Mission>();
        for (int i = 0; i < missions.Count; i++)
        {
            clone.missions.Add(missions[i].GetClone());
        }

        return clone;
    }

}
