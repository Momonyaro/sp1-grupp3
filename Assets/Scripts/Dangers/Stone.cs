using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Hit hit;
    Animator anim;
    [Tooltip("How long until deleted after the falling animation is triggered")]
    public float deleteTimer = 1f;

    void Start()
    {
        hit = FindObjectOfType<Hit>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetBool("Hit", true);
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, deleteTimer);
            hit.KnockingBack();
            Debug.Log("Hit stone");
        }
    }
}
