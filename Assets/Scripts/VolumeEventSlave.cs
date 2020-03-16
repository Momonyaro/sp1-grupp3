using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeEventSlave : MonoBehaviour
{
    VolumeEventMaster _master;
    bool _lockedToEvent = false;

    private void Awake()
    {
        if (OptionManager.GetFloatIfExists("soundVolume") != float.MinValue)
        GetComponent<AudioSource>().volume = OptionManager.GetFloatIfExists("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        if (_master == null && FindObjectOfType<VolumeEventMaster>())
        {
            _master = FindObjectOfType<VolumeEventMaster>();
        }
        else if (!_lockedToEvent && _master != null)
        {
            _master.onVolumeChange += OnVolumeChange;
            _lockedToEvent = true;
        }
    }

    private void OnVolumeChange(float newVolume)
    {
        GetComponent<AudioSource>().volume = newVolume;
    }

    private void OnDestroy()
    {
        if (_master != null)
            _master.onVolumeChange -= OnVolumeChange;
    }
}
