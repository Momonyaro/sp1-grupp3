using System;
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
        //wpm.Move();
        transform.Translate(Vector2.up * Time.deltaTime * eelSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
