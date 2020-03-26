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
            Destroy(GetComponent<Collider2D>());
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupPlank);
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }
            TextManager.plankAmount += collectableScore;
            Destroy(gameObject);
        }
    }
}
