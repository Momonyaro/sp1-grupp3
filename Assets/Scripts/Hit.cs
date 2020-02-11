using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] float knockbackPower = 500f;
    BoatMovementV01 boat;
    bool croc = false;
    string player = "Player";
    bool countingTime = false;
    float counter = 0f;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {

    }

    public void StoneHit(Collider2D collision, GameObject stone) //knockback lite, sten dör
    {
        if(collision.tag == player)
        {
            Destroy(stone);

            //boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -knockbackPower));
            //animation sten går sönder
            Debug.Log("Hit by stone");
        }
    }

    public void CrocHit(Collider2D collision)
    {
        if(collision.tag == player)
        {
            croc = true;
            Debug.Log("Player hit by croc");
        }
    }
}
