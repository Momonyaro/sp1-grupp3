using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] float knockbackPower = 500f;
    [Tooltip("In seconds")]
    [SerializeField] float knockbackTime = 2f;
    BoatMovementV01 boat;
    string player = "Player";

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {

    }

    public void StoneHit(Collider2D collision, GameObject stone)
    {
        if(collision.tag == player)
        {
            Destroy(stone);
            boat.knockbackBoolSwitch();
            StartCoroutine(Knockback());
            Debug.Log("Hit by stone");
        }
    }

    public void CrocHit(Collider2D collision)
    {
        if(collision.tag == player)
        {
            boat.knockbackBoolSwitch();
            StartCoroutine(Knockback());
            Debug.Log("Player hit by croc");
        }
    }

    IEnumerator Knockback()
    {
        boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -knockbackPower));
        yield return new WaitForSeconds(knockbackTime);
        boat.knockbackBoolSwitch();
    }
}
