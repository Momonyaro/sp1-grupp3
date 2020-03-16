using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public enum GoalType
    {
        ReachGoal, CollectMission, CollectPlanks
    }
    public GoalType goalType;
    [Space]
    [SerializeField] string loadScene;
    public GameObject smartCamera;
    public float victoryTime = 2;
    [SerializeField] float timeChecker = 0;

    private bool finished = false;
    public static bool reachedGoal = false;

    public AudioSource goalJingle;

    public GameObject successText;
    public GameObject lostText;
    [Space]
    public GameObject commonParent;
    public GameObject missionParent;
    public GameObject plankParent;
    public GameObject healthParent;
    public GameObject resultParent;
    public GameObject resultBack;
    [Space]
    public GameObject commonResultPos;
    public GameObject missionResultPos;
    public GameObject plankResultPos;

    TextManager textm;
    private void Start()
    {
        textm = FindObjectOfType<TextManager>();
        finished = false;
    }

    private void Update()
    {
        if (reachedGoal && timeChecker < victoryTime)
        {
            timeChecker += Time.deltaTime;

            if (timeChecker > victoryTime)
            {
                commonParent.transform.position = commonResultPos.transform.position;

                missionParent.transform.position = missionResultPos.transform.position;

                plankParent.transform.position = plankResultPos.transform.position;

                healthParent.SetActive(false);
                resultBack.SetActive(true);

                if (goalType == GoalType.ReachGoal)
                {
                    ShowSuccessText();
                }

                if (goalType == GoalType.CollectMission)
                {
                    if (textm.requiredMissionAmount <= TextManager.missionAmount)
                    {
                        ShowSuccessText();
                        Debug.Log(textm.requiredMissionAmount);
                    }
                    else if (textm.requiredMissionAmount > TextManager.missionAmount)
                    {
                        lostText.SetActive(true);
                    }
                }

                if (goalType == GoalType.CollectPlanks)
                {
                    if(textm.requiredPlankAmount <= TextManager.plankAmount)
                    {
                        ShowSuccessText();
                        Debug.Log(textm.requiredPlankAmount);
                    }
                    else if (textm.requiredPlankAmount > TextManager.plankAmount)
                    {
                        lostText.SetActive(true);
                    }
                }
                finished = true;
                reachedGoal = false;
            }
        }

        //Time.timeScale = (finished) ? .3f : 1;
       
    }

    public void LoadMap(string mapName)
    {
        var thisScene = mapName;
        if (mapName.Equals("this")) thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            smartCamera.SetActive(false);
            if(goalJingle != null)
            {
                goalJingle.Play();
            }
            reachedGoal = true;
        }
    }

    private void ShowSuccessText()
    {
        successText.SetActive(true);
        Time.timeScale = 0.3f;
        OptionManager.SetIntPreference(SceneManager.GetActiveScene().name, 1);
    }
}
