using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Common,
    Mission
}

public class Collectable : MonoBehaviour
{
    public AudioSource commonCollectSound;
    public AudioSource missionCollectSound;
    public int collectableScore = 1;
    public CollectableType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(type == CollectableType.Common)
            {
                if (commonCollectSound != null)
                {
                    commonCollectSound.Play();
                }
                TextManager.score += collectableScore;
                Destroy(gameObject);
            }
            if (type == CollectableType.Mission)
            {
                if (missionCollectSound != null)
                {
                    missionCollectSound.Play();
                }
                TextManager.missionAmount += collectableScore;
                Destroy(gameObject);
            }
        }
    }
}