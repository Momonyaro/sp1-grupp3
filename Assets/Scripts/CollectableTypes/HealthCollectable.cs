using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    public int healing = 1;
    BoatMovementV01 boatMv;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupFly);
            boatMv = other.GetComponent<BoatMovementV01>();

            if(BoatMovementV01.currentHealth < boatMv.maxHealth)
            {
                BoatMovementV01.currentHealth += healing;
                Debug.Log("Heald for one. Current health;" + BoatMovementV01.currentHealth);
            }

            Destroy(gameObject);
        }
    }
}
