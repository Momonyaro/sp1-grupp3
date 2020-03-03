using Managers;
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
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupEgg);
            if(fullHealthImage && emptyHealthImage != null)
            {
                fullHealthImage.SetActive(true);
                emptyHealthImage.SetActive(false);
            }
            else
            {
                fullHealthImage = Resources.Load<GameObject>("stop");
                emptyHealthImage = Resources.Load<GameObject>("stop");
            }
            TextManager.missionAmount += 1;
            Destroy(gameObject);
        }
    }
}
