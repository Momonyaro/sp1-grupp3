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
    [SerializeField] AudioSource growl;
    Hit hit;
    bool x = false;
    float basicTimer = 3f;

    public object UnityEgnine { get; private set; }

    void Start()
    {
        hit = FindObjectOfType<Hit>();
    }

    void Update()
    {
        Move();
        MakingSounds();
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
    private void MakingSounds()
    {
        basicTimer -= Time.deltaTime;
        if(basicTimer <= 0)
        {
            float timer = UnityEngine.Random.Range(1, 4); Debug.Log("Timer: " + timer);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(smallerPointX, transform.position.y - .5f, 0), new Vector3(biggerPointX, transform.position.y - .5f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hit.KnockingBack();
            Debug.Log("Hit croc");
        }
    }
}