using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadManager : MonoBehaviour
{
    //this is gonna be a hard-coded mess...

    public List<GameObject> firstActNpcs = new List<GameObject>();
    public List<GameObject> secondActNpcs = new List<GameObject>();
    public List<GameObject> thirdActNpcs = new List<GameObject>();

    private void Start()
    {
        SetAllInactive();
        bool canAddNPCs = true;
        foreach (GameObject gameObject in firstActNpcs)
        {
            NpcComponent npc = gameObject.GetComponent<NpcComponent>();
            if (npc.startsMission && canAddNPCs)
            {
                //if the level is completed then go on to check the next npcs
                if (OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) != int.MinValue && OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) == 1)
                {
                    break;
                }
                else
                {
                    Debug.Log("Set first act visible");
                    SetActActive(firstActNpcs);
                    canAddNPCs = false;
                }
            }
        }
        foreach (GameObject gameObject in secondActNpcs)
        {
            NpcComponent npc = gameObject.GetComponent<NpcComponent>();
            if (npc.startsMission && canAddNPCs)
            {
                //if the level is completed then go on to check the next npcs
                if (OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) != int.MinValue || OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) == 1)
                {
                    break;
                }
                else
                {
                    Debug.Log("Set second act visible");
                    SetActActive(secondActNpcs);
                    canAddNPCs = false;
                }
            }
        }
        foreach (GameObject gameObject in thirdActNpcs)
        {
            NpcComponent npc = gameObject.GetComponent<NpcComponent>();
            if (npc.startsMission && canAddNPCs)
            {
                //if the level is completed then go on to check the next npcs
                if (OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) != int.MinValue || OptionManager.GetIntIfExists(npc.missionComponent.targetSceneName) == 1)
                {
                    break;
                }
                else
                {
                    Debug.Log("Set third act visible");
                    SetActActive(thirdActNpcs);
                    canAddNPCs = false;
                }
            }
        }
    }

    private void SetAllInactive()
    {
        foreach(GameObject gameObject in firstActNpcs)
        {
            gameObject.SetActive(false);
        }
        foreach(GameObject gameObject in secondActNpcs)
        {
            gameObject.SetActive(false);
        }
        foreach(GameObject gameObject in thirdActNpcs)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetActActive(List<GameObject> gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }

}
