using HeavenFalls.Inventory;
using HeavenFalls.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HeavenFalls.UI
{
    public class WeaponScreen : MonoBehaviour, ISlotManager
    {
        public TMP_Text nameText;
        public TMP_Text levelText;
        public SO_WeaponData so_weapon;
        public GeneralSlot prefab;
        public Transform slot_parent;
        public List<GeneralSlot> slots;

        public UpgradeStatWeaponControlView upgradeStatControlView;
        public Button button_upgrade;

        [HideInInspector] public Weapon.WeaponDetail weaponSelected;


        public void Initialize()
        {
            if (so_weapon.weapons.Count <= 0) return;

            ResetData();
            for (int i = 0; i < so_weapon.weapons.Count; i++)
            {
                if (slots.Count > i)
                {
                    slots[i].data.id = so_weapon.weapons[i].id;
                    slots[i].data.name = so_weapon.weapons[i].name;
                    slots[i].Initialize();
                    slots[i].gameObject.SetActive(true);
                }
                else
                {
                    GeneralSlot slot = Instantiate(prefab, slot_parent);
                    slot.data = new Item();
                    slot.data.id = so_weapon.weapons[i].id;
                    slot.data.name = so_weapon.weapons[i].name;
                    slot.Initialize();
                    slot.onSelect += SelectWeapon;
                    slots.Add(slot);
                }
            }

            SelectWeapon(1);
        }

        private void SelectWeapon(int id)
        {
            weaponSelected = so_weapon.GetWeaponById(id);
            nameText.text = weaponSelected.name;
            levelText.text = "" + weaponSelected.config.level;
            button_upgrade.gameObject.SetActive(weaponSelected.config.isCanUpgrade);

            //perlihatkan stat karakter saat ini
            upgradeStatControlView.stat_base = weaponSelected.config.stats;
            upgradeStatControlView.InitializeStatBase();

            //perlihatkan preview upgrade level
            upgradeStatControlView.stat_upgrade = weaponSelected.config.GetPreviewStatNextLevel();
            upgradeStatControlView.InitializeStatToUpgrade();


            //update weapon selected di game manager
            if (GameManager.instance != null) GameManager.instance.weaponId_selected = id;
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
            weaponSelected.config.UpgradeStatNextLevel();
            SelectWeapon(weaponSelected.id);
        }

    }
}