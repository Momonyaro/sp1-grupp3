using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject optionsButton;

    public static bool gameIsPaused = false;

    [Tooltip("Write the name of the current scene")]
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (FinishLine.reachedGoal)
        {
            optionsButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !FinishLine.reachedGoal)
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
}
