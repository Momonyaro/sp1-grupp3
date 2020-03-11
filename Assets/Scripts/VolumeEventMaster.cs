using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnVolumeChange(float newVolume);

public class VolumeEventMaster : MonoBehaviour
{
    public event OnVolumeChange onVolumeChange;

    public void ChangeVolume(float newVolume)
    {
        onVolumeChange(newVolume);
    }
}
