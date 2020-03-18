using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpBox : MonoBehaviour
{
    public GameObject child;
    public Text titleText;
    public Text descText;

    public void CreateHelpBox(string title, string desc)
    {
        titleText.text = title;
        descText.text = desc;
        Time.timeScale = 0;
        child.SetActive(true);
    }

    public void CloseHelpBox()
    {
        Time.timeScale = 1;
        child.SetActive(false);
    }
}
