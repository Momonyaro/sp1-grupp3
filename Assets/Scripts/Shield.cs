using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    BoatMovementV01 boat;
    Hit hit;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        hit = FindObjectOfType<Hit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boat.shield = true;
            hit.ShieldSwitchBool();
            Destroy(gameObject);
        }
    }
}
