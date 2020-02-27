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
    [SerializeField] AudioSource bite;
    Hit hit;
    public bool direction = false;
    float basicTimer = 1f;
    bool x = false;

    void Start()
    {
        hit = FindObjectOfType<Hit>();
    }

    void Update()
    {
        Move();
        //MakingSounds();
    }

    private void Move()
    {
        if (transform.position.x >= biggerPointX)
        {
            direction = true;
            x = true;
            Rotate();
            
        }
        else if (transform.position.x <= smallerPointX)
        {
            direction = false;
            x = true;
            Rotate();
        }

        if (direction)
        {
            transform.Translate(-Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }
    }

    private void Rotate()
    {
        if(x)
        {
            transform.Rotate(0, 180, 0);
            x = false;
        }
    }

    private void MakingSounds()
    {
        //basicTimer -= Time.deltaTime;
        //if(basicTimer <= 0)
        //{
        //    basicTimer = 10f;
        //    float timer = UnityEngine.Random.Range(1, 4); Debug.Log("Timer: " + timer);
        //    timer -= Time.deltaTime;
        //    Debug.Log("In first one");
        //    if (timer <= 0)
        //    {
        //        Instantiate(growl, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //        Destroy(growl, 3f);
        //        Debug.Log("sound playing");
        //    }
        //}
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
            var sound = Instantiate(bite, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(sound, 3f);
            Debug.Log("Hit croc");
        }
    }
}