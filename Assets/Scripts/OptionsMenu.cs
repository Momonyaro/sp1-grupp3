using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject optionsButton;

    public static bool gameIsPaused = false;

    [Tooltip("Write the name of the current scene")]
    public string sceneName;

    public GameObject toggle;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;

    }

    void Update()
    {
        if (FinishLine.reachedGoal)
        {
            optionsButton.SetActive(false);
        }

        bool optionLock = false;
        if (optionsButton.activeInHierarchy && optionsButton.GetComponent<Button>().interactable)
        {
            optionLock = true;
        }

        if (optionLock && Input.GetKeyDown(KeyCode.Escape) && !FinishLine.reachedGoal)
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause(); 
            }
        }
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        if (OptionManager.GetIntIfExists("language") != int.MinValue)
        {
            switch (OptionManager.GetIntIfExists("language"))
            {
                case 0: //Swedish
                    toggle.GetComponent<Toggle>().isOn = false;
                    break;
                case 1: //English
                    toggle.GetComponent<Toggle>().isOn = true;
                    break;
            }
        }
    }

    public void Restart()
    {
        FinishLine.reachedGoal = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stad");
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LanguageTog(bool value)
    {
        if (value)
        {
            OptionManager.SetIntPreference("language", 1);
        }
        else
        {
            OptionManager.SetIntPreference("language", 0);
        }
    }
}
