using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCorner : MonoBehaviour
{
    CollisionHandeler ch;
    [Tooltip("When in doubt, skriv samma nummer som står i namnet")]
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
