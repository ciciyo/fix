using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{


    public List<GameObject> characters;

    public GameObject last_selected;

    // Start is called before the first frame update
    void Start()
    {
        SelectById(1);
    }
    
    public void SelectById(int id)
    {
        if (last_selected != null)
        {
            if(last_selected != characters[id - 1]) last_selected.SetActive(false);

        }
        last_selected = characters[id - 1];
        last_selected.SetActive(true);
    }


    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) SelectById(1);
        if(Input.GetKeyDown(KeyCode.Alpha2)) SelectById(2);
        if(Input.GetKeyDown(KeyCode.Alpha3)) SelectById(3);
        if(Input.GetKeyDown(KeyCode.Alpha4)) SelectById(4);
        if(Input.GetKeyDown(KeyCode.Alpha5)) SelectById(5);
        if(Input.GetKeyDown(KeyCode.Alpha6)) SelectById(6);
    }*/
}
