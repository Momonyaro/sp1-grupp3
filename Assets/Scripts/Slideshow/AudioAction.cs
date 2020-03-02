using System.Collections.Generic;
using UnityEngine;

namespace Slideshow
{
    [RequireComponent(typeof(AudioSource))]
    
    public class AudioAction : Action
    {
        public AudioClip clip;
        public float volume = .3f;

        public override void OnAction()
        {
            AudioSource src = GetComponent<AudioSource>();

            src.loop = false;
            src.playOnAwake = false;
            src.clip = clip;
            src.volume = volume;
            src.Play();
            
            foreach (var sibling in siblings)
            {
                sibling.OnAction();
            }
        }
    }
}
