using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileControlView : MonoBehaviour
{
    public Image icon_avatar;
    public TMP_Text usernameText;
    public TMP_Text levelText;
    public TMP_Text coinText;
    public TMP_Text diamondText;


    private void OnEnable()
    {
        InitializeProfile();
    }

    private void InitializeProfile()
    {
        if (PlayerData.instance == null) return;
        Profile profile = PlayerData.instance.profile;
        if(usernameText) usernameText.text = profile.username;
        if(levelText) levelText.text = "" + profile.level;
        if(coinText) coinText.text = $"{profile.coin:N0}";
        if(diamondText) diamondText.text = $"{profile.diamond:N0}";
        if(icon_avatar) icon_avatar.sprite = profile.avatar;
    }
}
