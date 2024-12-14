using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlexusTechdev.Utility
{
    public class TabManager : MonoBehaviour
    {
        public Button_tab[] tabButtons; // Array untuk menyimpan referensi ke setiap button
        public GameObject[] panels; // Array untuk menyimpan referensi ke setiap panel

        public Transform pointer;
        public Ease ease_move;

        [HideInInspector] public int current_index = 0;

        bool active = false;
        public float duration_changeTab = 0.15f;
        private Button_tab last_button_tab;
        public bool initialize_onStart = true;

        private void Start()
        {
            if(initialize_onStart) Initialize();
            
        }


        public void Initialize()
        {
            // Menambahkan listener ke setiap button
            for (int i = 0; i < tabButtons.Length; i++)
            {
                tabButtons[i].id = i;
                tabButtons[i].tabManager = this;
            }

            ShowPanel(0);
            SelectButton(tabButtons[0]);
        }

        public void SelectButton(Button_tab buttonTab)
        {
            /*for(int i = 0;  i < tabButtons.Length; i++)
            {
                if (tabButtons[i] != buttonTab) tabButtons[i].UnSelected();
            }*/

            if (last_button_tab) last_button_tab.Invoke("UnSelected", duration_changeTab);
            last_button_tab = buttonTab;

            ShowPanel(buttonTab.id);
            MovePointer(buttonTab.transform);
        }

        private void MovePointer(Transform point)
        {
            pointer.DOMoveX(point.position.x, duration_changeTab).SetEase(ease_move);
        }

        public void ShowPanel(int index)
        {
            HideAllPanels(); // Menyembunyikan semua panel
            if (index >= 0 && index < panels.Length)
            {
                panels[index].SetActive(true); // Menampilkan panel yang dipilih
            }
        }

        private void HideAllPanels()
        {
            foreach (var panel in panels)
            {
                panel.SetActive(false); // Menyembunyikan setiap panel
            }
        }


        // public void SwitchTabLeft(InputAction.CallbackContext context)
        // {
        //     //Debug.Log("call switch left");
        //     if (context.phase == InputActionPhase.Performed)
        //     {
        //         current_index = Mathf.Clamp(current_index - 1, 0, tabButtons.Length-1);
        //         tabButtons[current_index].Select();
        //     }
        // }
        //
        // public void SwitchTabRight(InputAction.CallbackContext context)
        // {
        //     //Debug.Log("call switch right");
        //     if (context.phase == InputActionPhase.Performed)
        //     {
        //         current_index = Mathf.Clamp(current_index + 1, 0, tabButtons.Length-1);
        //         tabButtons[current_index].Select();
        //     }
        // }
    }
}