﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEel : MonoBehaviour
{
    [SerializeField] float eelSpeed = 2f;
    public float stunTime = 2f;
    BoatMovementV01 boat;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * eelSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && boat.StunStatus() == false)
        {
            boat.StunnedBoolSwitch();
            boat.GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(Stunned());
        }
    }

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(stunTime);
        boat.GetComponent<SpriteRenderer>().color = Color.white;
        boat.StunnedBoolSwitch();
    }
}
