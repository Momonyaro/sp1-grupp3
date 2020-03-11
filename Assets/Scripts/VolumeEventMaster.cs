using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnVolumeChange(float newVolume);

public class VolumeEventMaster : MonoBehaviour
{
    public event OnVolumeChange onVolumeChange;

    public void ChangeVolume(float newVolume)
    {
        if (onVolumeChange != null)
            onVolumeChange(newVolume);

        OptionManager.SetFloatPreference("soundVolume", newVolume);
    }

    private void Awake()
    {
        if (OptionManager.GetFloatIfExists("soundVolume") != float.MinValue)
            GetComponent<Slider>().SetValueWithoutNotify(OptionManager.GetFloatIfExists("soundVolume"));
    }
}
