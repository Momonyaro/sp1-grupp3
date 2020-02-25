using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] string loadScene;
    public GameObject smartCamera;
    public float victoryTime = 2;
    [SerializeField] float timeChecker = 0;

    private bool finished = false;
    private bool reachedGoal = false;

    public AudioSource goalJingle;

    public GameObject successText;
    public GameObject commonParent;
    public GameObject missionParent;
    public GameObject plankParent;
    public GameObject healthParent;
    public GameObject resultParent;
    public GameObject resultBack;

    private void Update()
    {
        if (reachedGoal && timeChecker < victoryTime)
        {
            timeChecker += Time.deltaTime;

            if (timeChecker > victoryTime)
            {
                commonParent.transform.position = resultParent.transform.position + new Vector3(0, 150, 0);
                commonParent.transform.SetParent(resultParent.transform);

                missionParent.transform.SetParent(resultParent.transform);
                missionParent.transform.position = resultParent.transform.position + new Vector3(80, 0, 0);

                plankParent.transform.SetParent(resultParent.transform);
                plankParent.transform.position = resultParent.transform.position + new Vector3(0, -40, 0);

                healthParent.SetActive(false);
                resultBack.SetActive(true);
                successText.SetActive(true);

                finished = true;
            }
        }
        if (finished && Input.anyKeyDown)
        {
            timeChecker = 0;
            OptionManager.SetIntPreference(SceneManager.GetActiveScene().name, 1);
            SceneManager.LoadScene(loadScene);
        }
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
}
