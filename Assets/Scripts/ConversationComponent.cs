using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum Language
{
    Swedish,
    English
}

public enum PortraitExpression
{
    Happy,
    Angry,
    Sad,
    Surprised
}

public class ConversationComponent : MonoBehaviour
{
    [SerializeField] string npcName;
    public Language currentDialogueLanguage = Language.English;
    [FormerlySerializedAs("ScrollSpeed")]
    [Range(0, 0.4f)]
    [SerializeField] private float scrollSpeed = 0.03f;
    private float _timer;
    private string _currentString;
    private int _stringIndex;
    private int _currentLineIndex = 0;
    [SerializeField] private List<PortraitBlock> portraits = new List<PortraitBlock>();
    [SerializeField] private List<DialogueBlock> npcLines = new List<DialogueBlock>();
    bool _finishedBuilding = false;
    [SerializeField] private Text textBox;
    [SerializeField] private Text nameBox;
    [SerializeField] private Image portraitFrame;

    private void Awake()
    {
        _timer = scrollSpeed;
        ClearTextBox();
        nameBox.text = npcName;
        portraitFrame.sprite = FetchPortrait(npcLines[_currentLineIndex].portraitExpression);
        if (currentDialogueLanguage == Language.English) _currentString = npcLines[_currentLineIndex].englishText;
        else if (currentDialogueLanguage == Language.Swedish) _currentString = npcLines[_currentLineIndex].swedishText;
    }


    private void Update()
    {
        if (_timer <= 0 && !_finishedBuilding)
        {
            PlaceNextChar();
            _timer = scrollSpeed;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    public void StartBuildingNextString(bool trigger)
    {
        if (trigger)
        {
            _currentLineIndex++;
            if (_currentLineIndex < npcLines.Count)
            {
                ClearTextBox();
                _finishedBuilding = false;
                nameBox.text = npcName;
                portraitFrame.sprite = FetchPortrait(npcLines[_currentLineIndex].portraitExpression);
                if (currentDialogueLanguage == Language.English) _currentString = npcLines[_currentLineIndex].englishText;
                else if (currentDialogueLanguage == Language.Swedish) _currentString = npcLines[_currentLineIndex].swedishText;
                _stringIndex = 0;
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

    private Sprite FetchPortrait(PortraitExpression expression)
    {
        if (portraits.Count <= 0)
            Debug.LogError("No Portraits Found! Please Check: " + npcName);

        foreach (var portraitBlock in portraits.Where(portraitBlock => portraitBlock.portraitExpression == expression))
        {
            return portraitBlock.portraitSprite;
        }

        return portraits[0].portraitSprite;
    }

    private void PlaceNextChar()
    {
        if (_stringIndex < _currentString.Length)
        {
            textBox.text += GetNextChar(_stringIndex);
        }
        else
        {
            _finishedBuilding = true;
        }
    }

    private string GetNextChar(int index)
    {
        _stringIndex++;
        return _currentString.Substring(index, 1);
    }
}

[System.Serializable]
public struct PortraitBlock
{
    public PortraitExpression portraitExpression;
    public Sprite portraitSprite;
}

[System.Serializable]
public struct DialogueBlock
{
    [TextArea(0, 5)]
    public string swedishText;
    [TextArea(0, 5)]
    public string englishText;
    public PortraitExpression portraitExpression;
}
