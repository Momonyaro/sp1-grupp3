using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapKnockback : MonoBehaviour
{
    Shaker shaker;
    BoatMovementV01 boat;
    void Start()
    {

    }

    void Update()
    {

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var newDirection = GetComponent<Rigidbody2D>().transform.position - collision.transform.position;
    //    //StartCoroutine(shaker.Shake());
    //    boat.StartCoroutine(boat.AccurateKnockback(newDirection));
    //    Debug.Log("Collided with tilemap");
    //}
}
