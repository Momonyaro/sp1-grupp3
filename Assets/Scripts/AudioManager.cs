﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public enum Sounds
    {
        BoatCrash, StoneCrash,
        CrocodileGrowl, CrocodileBite, Whirlpool,
        PickupEgg, PickupPlank, PickupFly, CoinPickup,
        Tongue, TongueCatch,
        Dash, Brake, Achievement,
    }

    public delegate void RequestSoundDelegate(Sounds sound);

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioObject> soundObjects = new List<AudioObject>();
        [SerializeField] public List<GameObject> audioChannels = new List<GameObject>();
        public RequestSoundDelegate requestSoundDelegate;

        [Range(0, 1)]
        public float generalVolume = 1f;

        private void Awake()
        {
            requestSoundDelegate = AttemptToPlaySound;
            if (OptionManager.GetFloatIfExists("soundVolume") != float.MinValue)
                generalVolume = OptionManager.GetFloatIfExists("soundVolume");
        }

        private void AttemptToPlaySound(Sounds sound)
        {
            Debug.Log("Recieved request to play " + sound.ToString());
            foreach (var soundObject in soundObjects.Where(soundObject => soundObject.soundName == sound))
            {
                TetherAudio(AttemptAudioTether(soundObject), soundObject);
            }
        }

        private AudioSource AttemptAudioTether(AudioObject sound)
        {
            AudioSource toReturn = null;
            foreach(GameObject channel in audioChannels)
            {
                AudioSource channelSource = channel.GetComponent<AudioSource>();
                if (channelSource.isPlaying && channelSource.clip != null)
                {
                    if (channelSource.clip.name.Equals(sound.soundClip.name))
                        return null;
                }
                else
                    toReturn = channelSource;
            }

            return toReturn != null ? toReturn : null;
        }

        private void TetherAudio(AudioSource channelSource, AudioObject soundObject)
        {
            if (channelSource != null)
            {
                channelSource.volume = soundObject.soundVolume * generalVolume;
                channelSource.clip = soundObject.soundClip;
                channelSource.Play();
            }
        }

        public void SetVolume(float newVolume)
        {
            generalVolume = newVolume;
        }
    }

    [System.Serializable]
    public struct AudioObject
    {
        public AudioClip soundClip;
        [Range(0, 1)]
        public float soundVolume;
        public Sounds soundName;
    }
}