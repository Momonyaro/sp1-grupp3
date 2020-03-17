using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    public string swedishText;
    public string englishText;

    public Language language;
    public Text _text;

    private void Update()
    {
        if (OptionManager.GetIntIfExists("language") != int.MinValue)
        {
            language = (Language)OptionManager.GetIntIfExists("language");
        }

        if (language == Language.Swedish)
        {
            _text.text = swedishText;
        }
        if (language == Language.English)
        {
            _text.text = englishText;
        }
    }
}
