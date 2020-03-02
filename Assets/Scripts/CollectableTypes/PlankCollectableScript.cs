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
                collectSound.Play();
            }
            TextManager.plankAmount += collectableScore;
            Destroy(gameObject);
        }
    }
}
