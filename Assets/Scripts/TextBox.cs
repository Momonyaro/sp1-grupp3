using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public GameObject panel;
    public Text nameBox;
    public Text textBox;
    public Image portraitFrame;
    public Button optionsButton;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void SetDialogueWindowVisibility(bool active)
    {
        optionsButton.interactable = !active;
        panel.SetActive(active);
        portraitFrame.gameObject.SetActive(active);
    }
}
