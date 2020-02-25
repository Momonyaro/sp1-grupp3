using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] float knockbackPower = 500f;
    [Tooltip("In seconds")]
    [SerializeField] float knockbackTime = 2f;
    [SerializeField] GameObject shieldSprite = null;
    //public Animation anim;
    GameObject myShield;

    BoatMovementV01 boat;
    string player = "Player";
    bool shield = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        //anim = GetComponent<Animation>();
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
        else if (!shield)
        {
            Destroy(myShield);
            Debug.Log("Shield destroyed");
        }
    }

    public void StoneHit(Collider2D collision, GameObject stone)
    {
        if(collision.tag == player)
        {
            Destroy(stone.GetComponent<Collider2D>());
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

    IEnumerator Knockback()
    {
        boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -knockbackPower));
        yield return new WaitForSeconds(knockbackTime);
        boat.KnockbackBoolSwitch();
        boat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
