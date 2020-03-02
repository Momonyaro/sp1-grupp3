using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public delegate void RequestSoundDelegate(string soundName);

    public class AudioManager : MonoBehaviour
    {
        public double soundVolume = 0.8;
        [SerializeField] private List<AudioObject> soundObjects = new List<AudioObject>();
        [SerializeField] public List<GameObject> audioChannels = new List<GameObject>();
        public RequestSoundDelegate requestSoundDelegate;

        private void Awake()
        {
            requestSoundDelegate = AttemptToPlaySound;
        }

        private void Update()
        {
            if (soundVolume > 1) soundVolume = 1;
            foreach(GameObject channel in audioChannels)
            {
                channel.GetComponent<AudioSource>().volume = (float)soundVolume;
            }
        }

        private void AttemptToPlaySound(string soundName)
        {
            foreach (var soundObject in soundObjects.Where(soundObject => soundObject.name.Equals(soundName)))
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
                    {
                        return null;
                    }
                    else
                        toReturn = channelSource;
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
                channelSource.clip = soundObject.soundClip;
                channelSource.Play();
            }
        }
    }

    [System.Serializable]
    public struct AudioObject
    {
        public AudioClip soundClip;
        public string name;
    }
}