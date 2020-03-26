using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class Stone : MonoBehaviour
{
    Animator anim;
    BoatMovementV01 boat;
    [Tooltip("How long until deleted after the falling animation is triggered")]
    public float deleteTimer = 1f;
    bool triggered = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(triggered)
            Hit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            triggered = true;
            //Debug.Log("Stone should get destroyed");
            //Sound();
            //anim.SetBool("Hit", true);
            //Destroy(GetComponent<Collider2D>());
            //Destroy(gameObject, deleteTimer);
            //boat.KnockbackDangers(GetComponent<Collider2D>());
            //Debug.Log("Hit stone");
        }
    }

    private void Hit()
    {
        //Debug.Log("Stone should get destroyed");
        Sound();
        anim.SetBool("Hit", true);
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, deleteTimer);
        boat.KnockbackDangers(GetComponent<Collider2D>());
        Debug.Log("Hit stone");
        triggered = false;
    }
    private void Sound()
    {
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.StoneCrash);
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.BoatCrash);
    }
}
