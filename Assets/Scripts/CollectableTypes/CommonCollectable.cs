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
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.CoinPickup);
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }
            TextManager.score += collectableScore;
            TextManager.shieldCoinsAmount += 1;
            Shield.coinCount += 1;
            Debug.Log("Got a coin!" + Shield.coinCount);
            Destroy(gameObject);
        }
    }
}
