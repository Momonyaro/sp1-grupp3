using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerTextBox : MonoBehaviour
{
    public string swedishTitle;
    public string englishTitle;
    [TextArea(0, 5)]
    public string swedishText;
    [TextArea(0, 5)]
    public string englishText;

    private bool activated = false;

    public void CreateHelpBox()
    {
        Language currentLanguage = Language.English;
        if (OptionManager.GetIntIfExists("language") != int.MinValue)
        {
            currentLanguage = (Language)OptionManager.GetIntIfExists("language");
        }

        //Find UI and create textBox
        if (currentLanguage == Language.English)
            FindObjectOfType<HelpBox>().CreateHelpBox(englishTitle, englishText);
        if (currentLanguage == Language.Swedish)
            FindObjectOfType<HelpBox>().CreateHelpBox(swedishTitle, swedishText);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            CreateHelpBox();
            activated = true;
        }
    }
}
