using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    [SerializeField] float crocSpeed = 5f;
    [Tooltip("Det x-värde där krokodilen börjar åka till höger")]
    [SerializeField] float smallerPointX = 4f;
    [Tooltip("Det x-värde där krokodilen börjar åka till vänster")]
    [SerializeField] float biggerPointX = 7f;
    Hit hit;
    bool x = false;
    
    void Start()
    {
        hit = FindObjectOfType<Hit>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.x >= biggerPointX)
        {
            x = true;
        }
        if (transform.position.x <= smallerPointX)
        {
            x = false;
        }

        if (x)
        {
            transform.Translate(-Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(smallerPointX, transform.position.y - .5f, 0), new Vector3(biggerPointX, transform.position.y - .5f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit.CrocHit(collision);
    }
}