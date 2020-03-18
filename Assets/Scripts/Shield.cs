using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    BoatMovementV01 boat;
    Hit hit;
    public static int coinCount;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        hit = FindObjectOfType<Hit>();
    }

    private void Update()
    {
        if(coinCount >= 10)
        {
            Debug.Log("Shield activated");
            ActivateShield();
        }
    }

    public void ActivateShield()
    {
        boat.shield = true;
        hit.ShieldSwitchBool(true);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        boat.shield = true;
    //        hit.ShieldSwitchBool();
    //        Destroy(gameObject);
    //    }
    //}
}
