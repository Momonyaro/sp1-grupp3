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
    //[SerializeField] AudioSource falling = null;
    //[SerializeField] AudioClip falling = null;
    //[SerializeField] AudioClip crashing = null;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetBool("Hit", true);
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, deleteTimer);
            boat.KnockbackDangers(GetComponent<Collider2D>());
            Sound();
            Debug.Log("Hit stone");
        }
    }

    private void Sound()
    {
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.StoneCrash);
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.BoatCrash);

        //AudioSource.PlayClipAtPoint(falling, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        //AudioSource.PlayClipAtPoint(crashing, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
}
