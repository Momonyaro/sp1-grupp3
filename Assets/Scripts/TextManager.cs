﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject scoreText;
    public static int score;

    public GameObject gameOverText;
    public static bool gameOver = false;
    [Space]
    public GameObject plankText;
    public GameObject plankRequiredText;
    public static int plankAmount = 0;
    public int requiredPlankAmount = 10;
    [Space]
    public static int missionAmount;
    public int requiredMissionAmount = 3;

    private void Start()
    {
        if (plankRequiredText != null)
            plankRequiredText.GetComponent<Text>().text = "/ " + requiredPlankAmount;
        score = 0;
    }

    private void Update()
    {
        scoreText.GetComponent<Text>().text = ":" + score;

        if(gameOver)
        {
            gameOverText.SetActive(true);
        }
        else
        {
            gameOverText.SetActive(false);
        }

        if (plankText != null)
            plankText.GetComponent<Text>().text = "" + plankAmount;
    }
}