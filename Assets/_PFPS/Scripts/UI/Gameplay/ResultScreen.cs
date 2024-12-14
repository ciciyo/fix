using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResultScreen : MonoBehaviour
{
    public TMP_Text winText;


    public void ShowResultScreen(bool isWin)
    {
        
        winText.text = isWin ? "Quest Complete!" : "Game Over!";
    }


}
