using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riverfront : MonoBehaviour
{
    [SerializeField] float knockbackTime = 0.25f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var knockbackDirection = collision.transform.position - transform.position;
            StartCoroutine(Knockback(knockbackDirection, collision));
        }
    }
    private IEnumerator Knockback(Vector3 direction, Collision2D player)
    {
        
        yield return new WaitForSeconds(knockbackTime);
    }
}
