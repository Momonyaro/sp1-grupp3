using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankCollectableScript : Collectable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (collectSound != null)
            {
                FindObjectOfType<AudioManager>().requestSoundDelegate("pickupEgg");
            }
            TextManager.missionAmount += collectableScore;
            Destroy(gameObject);
        }
    }
}
