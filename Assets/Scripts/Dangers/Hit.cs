using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] float knockbackPower = 500f;
    [Tooltip("In seconds")]
    [SerializeField] float knockbackTime = 2f;
    [SerializeField] float stunTime = 2f;
    BoatMovementV01 boat;
    string player = "Player";

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {

    }

    public void StoneHit(Collider2D collision)
    {
        if(collision.tag == player)
        {
            boat.KnockbackBoolSwitch();
            StartCoroutine(Knockback());
            Debug.Log("Hit stone");
        }
    }

    public void CrocHit(Collider2D collision)
    {
        if(collision.tag == player)
        {
            boat.KnockbackBoolSwitch();
            StartCoroutine(Knockback());
            Debug.Log("Hit croc");
        }
    }

    public void EelHit(Collider2D collision)
    {
        if(collision.tag == "Player" && boat.StunStatus() == false)
        {
            boat.StunnedBoolSwitch();
            //boat.StopBoatSwitchBool();
            StartCoroutine(Stunned());
        }
    }


    IEnumerator Knockback()
    {
        boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -knockbackPower));
        yield return new WaitForSeconds(knockbackTime);
        boat.KnockbackBoolSwitch();
        boat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator Stunned()
    {
        //boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0));
        yield return new WaitForSeconds(stunTime);
        boat.StunnedBoolSwitch();
        //boat.StopBoatSwitchBool();
    }
}
