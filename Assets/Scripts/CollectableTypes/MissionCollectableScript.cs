using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCollectableScript : Collectable
{
    public GameObject fullMissionImage;
    public GameObject emptyMissionImage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.PickupEgg);
            if(pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            if (fullMissionImage && emptyMissionImage != null)
            {
                fullMissionImage.SetActive(true);
                emptyMissionImage.SetActive(false);
            }
            else
            {
                fullMissionImage = Resources.Load<GameObject>("stop");
                emptyMissionImage = Resources.Load<GameObject>("stop");
            }
            TextManager.missionAmount += 1;
            Destroy(gameObject);
        }
    }
}
