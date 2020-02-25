using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Hit hit;
    Animator anim;

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
            hit.KnockingBack();
            Debug.Log("Hit stone");
        }
    }
}
