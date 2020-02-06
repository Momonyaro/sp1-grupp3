using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationComponent : MonoBehaviour
{
    [SerializeField] Sprite npcSprite;
    [SerializeField] string npcName;
    [Range(0, 0.4f)]
    [SerializeField] float scrollSpeed = 0.03f;
    private float timer;
    private string currentString;
    private int stringIndex;
    private int currentLineIndex = 0;
    [SerializeField] List<string> npcLines = new List<string>();
    bool finishedBuilding = false;
    [SerializeField] Text textBox;

    private void Awake()
    {
        timer = scrollSpeed;
        ClearTextBox();
        currentString = npcLines[currentLineIndex];
    }

    private void Update()
    {
        if (timer <= 0 && !finishedBuilding)
        {
            PlaceNextChar();
            timer = scrollSpeed;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void StartBuildingNextString(bool trigger)
    {
        if (trigger)
        {
            currentLineIndex++;
            if (currentLineIndex < npcLines.Count)
            {
                ClearTextBox();
                finishedBuilding = false;
                currentString = npcLines[currentLineIndex];
                stringIndex = 0;
            }
            else
            {
                textBox.gameObject.SetActive(false);
            }
        }
    }

    private void ClearTextBox()
    {
        textBox.text = "";
    }

    private void PlaceNextChar()
    {
        if (stringIndex < currentString.Length)
        {
            textBox.text += GetNextChar(stringIndex);
        }
        else
        {
            finishedBuilding = true;
        }
    }

    private string GetNextChar(int index)
    {
        stringIndex++;
        return currentString.Substring(index, 1);
    }
}
