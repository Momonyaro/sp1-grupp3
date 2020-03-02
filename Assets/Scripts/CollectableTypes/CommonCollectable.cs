using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCollectable : Collectable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (collectSound != null)
            {
                FindObjectOfType<AudioManager>().requestSoundDelegate("pickupEgg");
            }
            TextManager.score += collectableScore;
            Destroy(gameObject);
        }
    }
}
