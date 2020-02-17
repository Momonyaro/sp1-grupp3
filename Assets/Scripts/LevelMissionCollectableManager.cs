using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMissionCollectableManager : MonoBehaviour
{
    public GameObject collectableThing1;
    public Image collectableImage1;
    public Image emptyCollectableImage1;

    public GameObject collectableThing2;
    public Image collectableImage2;
    public Image emptyCollectableImage2;

    public GameObject collectableThing3;
    public Image collectableImage3;
    public Image emptyCollectableImage3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (collectableThing1)
            {
                collectableImage1.enabled = true;
                emptyCollectableImage1.enabled = false;
            }
            if (collectableThing2)
            {
                collectableImage2.enabled = true;
                emptyCollectableImage2.enabled = false;
            }
            if (collectableThing3)
            {
                collectableImage3.enabled = true;
                emptyCollectableImage3.enabled = false;
            }
        }
    }
}
