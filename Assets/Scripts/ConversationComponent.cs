﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public string npcName;
    public List<AudioClip> voiceClips = new List<AudioClip>();
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
    private bool _finishedBuilding = true;
    private bool _dialogueComplete = false;
    [HideInInspector] public TextBox textBoxObject;
    private Text _textBox;
    private Text _nameBox;
    private Image _portraitFrame;
    public AudioSource vocalSource;

    private const float vocalChance = .4f;
    private const string PlayerCharName = "Poly";

    public void Update()
    {
        if (_timer <= 0 && !_finishedBuilding && CheckIfTethered())
        {
            PlaceNextChar();
            _timer = scrollSpeed;
        }
        else
        {
            _timer -= Time.unscaledDeltaTime;
        }
    }

    public bool StartConversation(TextBox textBoxObject)
    {
        if (OptionManager.GetIntIfExists("language") != int.MinValue)
        {
            switch (OptionManager.GetIntIfExists("language"))
            {
                case 0: //Swedish
                    currentDialogueLanguage = Language.Swedish;
                    break;
                case 1: //English
                    currentDialogueLanguage = Language.English;
                    break;
            }
        }
        TetherToTextbox(textBoxObject);
        if (_dialogueComplete == false)
        {
            StartBuildingNextString();
            FrogMovement.frozen = true;
            return true;
        }
        else
        {
            textBoxObject.SetDialogueWindowVisibility(false);
            ResetDialogue();
            UntetherTextbox();
            FrogMovement.frozen = false;
            return false;
        }
        
    }

    private AudioClip GetRandomVocal()
    {
        while (true)
        {
            foreach (var voiceClip in voiceClips)
            {
                if (vocalChance > Random.value)
                {
                    return voiceClip;
                }
            }
        }
    }

    public void TetherToTextbox(TextBox textBoxObject)
    {
        this.textBoxObject = textBoxObject;
        this._textBox = textBoxObject.textBox;
        this._nameBox = textBoxObject.nameBox;
        this._portraitFrame = textBoxObject.portraitFrame;
        _timer = scrollSpeed;
        textBoxObject.SetDialogueWindowVisibility(true);
    }

    public void UntetherTextbox()
    {
        this.textBoxObject = null;
        this._textBox = null;
        this._nameBox = null;
        this._portraitFrame = null;
    }

    private bool CheckIfTethered()
    {
        if (textBoxObject != null && _textBox != null &&
            _nameBox != null && _portraitFrame != null)
            return true;
        else 
            return false;
    }

    public void StartBuildingNextString()
    {
        Debug.Log("Starting new string");

        if (_currentLineIndex < npcLines.Count)
        {
            ClearTextBox();
            _finishedBuilding = false;
            if (npcLines[_currentLineIndex].playerSays)
            {
                _nameBox.text = PlayerCharName;
            }
            else
            {
                _nameBox.text = npcName;
                _portraitFrame.sprite = FetchPortrait(npcLines[_currentLineIndex].portraitExpression);
                if (!npcLines[_currentLineIndex].swedishText.StartsWith("."))
                {
                    vocalSource.clip = GetRandomVocal();
                    vocalSource.Play();
                }
            }
            if (currentDialogueLanguage == Language.English) _currentString = npcLines[_currentLineIndex].englishText;
            else if (currentDialogueLanguage == Language.Swedish) _currentString = npcLines[_currentLineIndex].swedishText;
            _stringIndex = 0;
            _currentLineIndex++;
        }
        
        if (_currentLineIndex >= npcLines.Count)
        {
            _dialogueComplete = true;
            _currentLineIndex = 0;
            _stringIndex = 0;
        }
    }

    private void ClearTextBox()
    {
        if (_textBox != null)
            _textBox.text = "";
    }

    public void ResetDialogue()
    {
        ClearTextBox();
        _currentLineIndex = 0;
        _stringIndex = 0;
        _dialogueComplete = false;
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
            string nextChar = GetNextChar(_stringIndex);
            _textBox.text += nextChar;
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

    public bool playerSays;
    public PortraitExpression portraitExpression;
}
