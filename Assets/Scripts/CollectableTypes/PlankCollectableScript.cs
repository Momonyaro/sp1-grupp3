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
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupPlank);
            TextManager.missionAmount += collectableScore;
            Destroy(gameObject);
        }
    }
}
