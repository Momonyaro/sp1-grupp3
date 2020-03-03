using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    public int healing = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupFly);

            if(BoatMovementV01.currentHealth < BoatMovementV01.maxHealth)
            {
                BoatMovementV01.currentHealth += healing;
                Debug.Log("Heald for one. Current health;" + BoatMovementV01.currentHealth);
            }

            Destroy(gameObject);
        }
    }
}
