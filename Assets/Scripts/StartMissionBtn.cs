using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class StartMissionBtn : MonoBehaviour
{
    public float loadTimeMax = 1;
    private float timer;
    private bool clicked = false;
    private MissionBox _missionBox;

    private void Start()
    {
        float loadTime = Random.Range(0, loadTimeMax);
        timer = loadTime;
        _missionBox = FindObjectOfType<MissionBox>();
    }

    private void Update()
    {
        if (clicked && timer <= 0)
        {
            OptionManager.SetIntPreference(_missionBox.NextSceneName, 0);
            SceneManager.LoadScene(_missionBox.NextSceneName);
        }
        else if (clicked && timer > 0)
            timer -= Time.deltaTime;
    }

    public void OnClick()
    {
        clicked = true;
        _missionBox.loadingScreen.SetActive(true);
    }
}
