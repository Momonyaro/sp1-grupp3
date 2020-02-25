using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Hit hit;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        hit = FindObjectOfType<Hit>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Hit", true);
        hit.StoneHit(collision, gameObject);
    }
}
