using HeavenFalls.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreen : MonoBehaviour, ISlotManager
{
    [Header("Character Select")]
    public TMP_Text nameText;
    //public TMP_Text characterTypeText;
    public Image characterType_img;
    public TMP_Text levelText;
    public TMP_Text characterDescription;
    public SO_CharacterData characterData;
    public SlotCharacter prefab;
    public Transform slot_parent;
    public List<SlotCharacter> slots;

    public UpgradeStatControlView upgradeStatControlView;
    public Button button_upgrade;
    public GameObject indicator_upgrade;
    public CharacterSelector characterSelector;

    [HideInInspector] public Character characterSelected;

    private SlotCharacter slot_selected;


    private void OnEnable()
    {
        Initialize();    
    }

    public void Initialize()
    {
        ResetData();
        for (int i = 0; i < characterData.characters.Count; i++)
        {
            if(slots.Count > i)
            {
                slots[i].data = characterData.characters[i];
                slots[i].Initialize();
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                SlotCharacter slot = Instantiate(prefab, slot_parent);
                slot.data = characterData.characters[i];
                slot.Initialize();
                slot.onSelect += SelectCharacter;
                slots.Add(slot);
            }
        }

        SelectCharacter(slots[0]);
    }

    public void SelectCharacter(SlotCharacter slot)
    {
        if (slot_selected != null) slot_selected.UpdateHighlight(false);

        slot_selected = slot;
        slot_selected.UpdateHighlight(true);

        characterSelected = characterData.GetCharacterById(slot.data.id);
        nameText.text = characterSelected.name;
        characterType_img.sprite = MainMenuManager.instance.gameData.characterTypeLibrary.GetCharacterTypeSprite(characterSelected.type);
        levelText.text = "Lv. " + characterSelected.config.level;
        characterDescription.text = characterSelected.description;

        //perlihatkan stat karakter saat ini
        upgradeStatControlView.stat_base = characterSelected.config.Stats;
        upgradeStatControlView.InitializeStatBase();

        //perlihatkan preview upgrade level
        //upgradeStatControlView.stat_upgrade = characterSelected.config.GetPreviewStatNextLevel();
        //upgradeStatControlView.InitializeStatToUpgrade();

        button_upgrade.gameObject.SetActive(characterSelected.config.isCanUpgrade);
        //indicator_upgrade.SetActive(characterSelected.config.isCanUpgrade);
    }


    public void PickCharacter()
    {
        //update character selected di game manager
        if (GameManager.instance != null) GameManager.instance.characterId_selected = slot_selected.data.id;
        characterSelector.SelectById(slot_selected.data.id);
        if (MainMenuManager.instance != null)
        {
            MainMenuManager.instance.SetCharacterSelected(characterSelected);
            MainMenuManager.instance.screenManager.OpenScreen("lobby", true);
        }
        
    }


    public void ResetData()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }


    public void UpgradeCharacter()
    {
        characterSelected.config.UpgradeStatNextLevel();
        SelectCharacter(slot_selected);
    }


}
