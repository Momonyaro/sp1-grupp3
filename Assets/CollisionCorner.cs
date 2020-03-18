using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCorner : MonoBehaviour
{
    CollisionHandeler ch;
    [SerializeField] int number;

    private void Start()
    {
        ch = FindObjectOfType<CollisionHandeler>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ch.KnockingBack(number);
    }
}
