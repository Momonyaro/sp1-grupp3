using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEel : MonoBehaviour
{
    [SerializeField] float eelSpeed = 2f;
    Hit hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = FindObjectOfType<Hit>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * eelSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit.EelHit(collision);
    }
}
