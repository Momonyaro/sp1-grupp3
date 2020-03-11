﻿using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCollectable : Collectable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.TongueCatch);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            TextManager.score += collectableScore;
            Destroy(gameObject);
        }
    }
}
