using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class MissionComponent
{
    public LanguageMissionText englishInfo;
    public LanguageMissionText swedishInfo;

    public Sprite missionPortrait;
    public string targetSceneName;
    private Language currentLanguage = Language.English;
    [HideInInspector]public MissionBox missionBox;

    public void TetherAndSetNewMission(MissionBox newMissionBox, string npcName)
    {
        if (OptionManager.GetIntIfExists("language") != int.MinValue)
        {
            switch (OptionManager.GetIntIfExists("language"))
            {
                case 0: //Swedish
                    currentLanguage = Language.Swedish;
                    break;
                case 1: //English
                    currentLanguage = Language.English;
                    break;
            }
        }
        if (newMissionBox != null)
            this.missionBox = newMissionBox;
        
        if (currentLanguage == Language.Swedish)
            this.missionBox.CreateMissionScreen(swedishInfo.missionName, swedishInfo.missionDescription, missionPortrait, targetSceneName, npcName);
        else 
            this.missionBox.CreateMissionScreen(englishInfo.missionName, englishInfo.missionDescription, missionPortrait, targetSceneName, npcName);
    }

    public void Update()
    {
        if (missionBox != null)
        {
            FrogMovement.frozen = missionBox.panel.activeInHierarchy;
        }
    }

    public bool MissionBoxActive()
    {
        return missionBox != null && missionBox.panel.activeInHierarchy;
    }
}

[System.Serializable]
public struct LanguageMissionText
{
    public string missionName;
    [TextArea(1, 7)]
    public string missionDescription;
}
