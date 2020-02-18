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
    [SerializeField] GameObject shieldSprite = null;
    GameObject myShield;
    BoatMovementV01 boat;
    string player = "Player";
    bool shield = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {
        if (shield)
        {
            myShield.transform.position = boat.transform.position;
        }
    }

    public void ShieldSwitchBool()
    {
        shield = !shield;
        if (shield)
        {
            myShield = Instantiate(shieldSprite, boat.transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(myShield);
            Debug.Log("Shield destroyed");
        }
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
            boat.GetComponent<SpriteRenderer>().color = Color.white;
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
        yield return new WaitForSeconds(stunTime);
        boat.GetComponent<SpriteRenderer>().color = Color.green; //new Color(85, 217, 125, 255);
        boat.StunnedBoolSwitch();
    }
}
