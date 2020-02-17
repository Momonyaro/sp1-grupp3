using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct MissionComponent
{
    public string missionName;
    [TextArea(1, 7)]
    public string missionDescription;

    public Sprite missionPortrait;
    public string targetSceneName;
    public GameObject missionUiPrefab;
}
