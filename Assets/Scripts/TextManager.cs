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

    public static int health;

    public GameObject missionText;
    public static int missionAmount;

    private void Start()
    {
        health = BoatMovementV01.maxHealth;
    }

    private void Update()
    {
        scoreText.GetComponent<Text>().text = ":" + score;

        if(gameOver)
        {
            gameOverText.SetActive(true);
        }

        missionText.GetComponent<Text>().text = ":" + missionAmount;
    }
}