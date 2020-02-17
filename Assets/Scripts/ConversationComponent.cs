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

[System.Serializable]
public class ConversationComponent
{
    [SerializeField] string npcName;
    public Language currentDialogueLanguage = Language.English;
    [FormerlySerializedAs("ScrollSpeed")]
    [Range(0, 0.4f)]
    [SerializeField] private float scrollSpeed = 0.03f;
    private float _timer;
    private string _currentString;
    [SerializeField]private int _stringIndex;
    [SerializeField] private int _currentLineIndex = 0;
    [SerializeField] private List<PortraitBlock> portraits = new List<PortraitBlock>();
    [SerializeField] private List<DialogueBlock> npcLines = new List<DialogueBlock>();
    [SerializeField]private bool _finishedBuilding = true;
    private TextBox _textBoxObject;
    private Text _textBox;
    private Text _nameBox;
    private Image _portraitFrame;


    public void Update()
    {
        if (_timer <= 0 && !_finishedBuilding)
        {
            PlaceNextChar();
            Debug.Log("timer reset");
            _timer = scrollSpeed;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    public void TetherToTextbox(TextBox textBoxObject)
    {
        this._textBoxObject = textBoxObject;
        this._textBox = textBoxObject.textBox;
        this._nameBox = textBoxObject.nameBox;
        this._portraitFrame = textBoxObject.portraitFrame;
        _timer = scrollSpeed;
        textBoxObject.SetDialogueWindowVisibility(true);
        
        StartBuildingNextString(true);
    }

    public void StartBuildingNextString(bool trigger)
    {
        Debug.Log("Starting new string");
        if (!trigger) return;
        if (_currentLineIndex < npcLines.Count)
        {
            ClearTextBox();
            _finishedBuilding = false;
            _nameBox.text = npcName;
            _portraitFrame.sprite = FetchPortrait(npcLines[_currentLineIndex].portraitExpression);
            if (currentDialogueLanguage == Language.English) _currentString = npcLines[_currentLineIndex].englishText;
            else if (currentDialogueLanguage == Language.Swedish) _currentString = npcLines[_currentLineIndex].swedishText;
            _stringIndex = 0;
            _currentLineIndex++;
        }
        else
        {
            Debug.Log("Hiding dialogue window.");
            _textBoxObject.SetDialogueWindowVisibility(false);
        }
    }

    private void ClearTextBox()
    {
        _textBox.text = "";
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
        if (_currentString != null && _stringIndex < _currentString.Length)
        {
            Debug.Log("placing character");
            _textBox.text += GetNextChar(_stringIndex);
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
