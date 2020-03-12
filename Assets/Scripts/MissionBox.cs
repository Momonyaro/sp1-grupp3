using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MissionBox : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject panel;
    public Text missionTitleBox;
    public Text missionDescBox;
    public Text npcNameBox;
    public Image missionPortraitFrame;
    [FormerlySerializedAs("NextSceneName")] public string nextSceneName = "NULL";

    private void Start()
    {
        SetWindowVisibility(false);
    }
    
    public void SetWindowVisibility(bool active)
    {
        panel.SetActive(active);
    }

    public void CreateMissionScreen(string missionTitle, string missionDescription, Sprite portrait, string targetScene, string npcName)
    {
        missionTitleBox.text = missionTitle;
        missionDescBox.text = missionDescription;
        missionPortraitFrame.sprite = portrait;
        nextSceneName = targetScene;
        npcNameBox.text = npcName;
        SetWindowVisibility(true);
    }
}
