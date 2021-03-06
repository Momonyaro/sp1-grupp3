﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Crocodile : MonoBehaviour
{
    [SerializeField] float crocSpeed = 5f;
    [Tooltip("Det x-värde där krokodilen börjar åka till höger")]
    [SerializeField] float smallerPointX = 4f;
    [Tooltip("Det x-värde där krokodilen börjar åka till vänster")]
    [SerializeField] float biggerPointX = 7f;
    Hit hit;
    BoatMovementV01 boat;
    public bool direction = false;
    float crocTimer = 0.5f;

    Animator animator;

    public ParticleSystem gnawEffect;
    public GameObject target;
    public float distance;
    //public float chaseTime = 2f;
    //private float chaseTimeCheck;
    //public float chaseCooldown = 3f;
    //private float chaseCooldownCheck;
    public float eyeSight = 5;

    void Start()
    {
        hit = FindObjectOfType<Hit>();
        boat = FindObjectOfType<BoatMovementV01>();
        //chaseTimeCheck = chaseTime;
        //chaseCooldownCheck = chaseCooldown;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        crocTimer -= Time.deltaTime;
        Move();
    }

    private void Move()
    {
        if (transform.position.x >= biggerPointX && crocTimer <= 0f)
        {
            direction = true;
            transform.Rotate(0, 180, 0);
            crocTimer = 0.5f;
        }
        if (transform.position.x <= smallerPointX && crocTimer <= 0f)
        {
            direction = false;
            transform.Rotate(0, 180, 0);
            crocTimer = 0.5f;
        }

        if (direction)
        {
            transform.Translate(-Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * crocSpeed), 0);
        }

        if(distance <= eyeSight)
        {
            animator.SetBool("Biting", true);
        }
        else
        {
            animator.SetBool("Biting", false);
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
            boat.KnockbackDangers(GetComponent<Collider2D>());
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.CrocodileBite);

            Debug.Log("Hit croc");
        }
    }
}