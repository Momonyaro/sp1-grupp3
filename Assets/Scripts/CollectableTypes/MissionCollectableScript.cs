using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCollectableScript : Collectable
{
    public GameObject fullHealthImage;
    public GameObject emptyHealthImage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (collectSound != null)
            {
                collectSound.Play();
            }
            fullHealthImage.SetActive(true);
            emptyHealthImage.SetActive(false);
            Destroy(gameObject);
        }
    }
}
