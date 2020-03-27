using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<OptionManager>() != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public static void SetIntPreference(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }

    //Returns the requested int if true otherwise it returns int.MinValue
    public static int GetIntIfExists(string name)
    {
        return PlayerPrefs.HasKey(name) ? PlayerPrefs.GetInt(name) : int.MinValue;
    }

    public static void SetFloatPreference(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }

    //Returns the requested float if true otherwise it returns float.MinValue
    public static float GetFloatIfExists(string name)
    {
        return PlayerPrefs.HasKey(name) ? PlayerPrefs.GetFloat(name) : float.MinValue;
    }

    public static void SetStringPreference(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }
    
    //Returns the requested string if true otherwise it returns null
    public static string GetStringIfExists(string name)
    {
        return PlayerPrefs.HasKey(name) ? PlayerPrefs.GetString(name) : null;
    }

    public static void ClearSavedInfo(bool keepConfig = true)
    {
        int language = GetIntIfExists("language");
        float volume = GetFloatIfExists("soundVolume");

        PlayerPrefs.DeleteAll();

        if (keepConfig)
        {
            if (language != int.MinValue)
                SetIntPreference("language", language);
            if (volume != float.MinValue)
                SetFloatPreference("soundVolume", volume);
        }
    }
}
