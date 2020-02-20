using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject scoreText;
    public static int score;

    public GameObject gameOverText;
    public static bool gameOver = false;

    public GameObject missionText;
    public GameObject missionRequiredText;
    public static int missionAmount;
    public int requiredAmount = 10;

    private void Start()
    {
        missionRequiredText.GetComponent<Text>().text = "/ " + requiredAmount;
    }

    private void Update()
    {
        scoreText.GetComponent<Text>().text = ":" + score;

        if(gameOver)
        {
            gameOverText.SetActive(true);
        }

        missionText.GetComponent<Text>().text = "" + missionAmount;
    }
}