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
        else if (!shield)
        {
            Destroy(myShield);
            Debug.Log("Shield destroyed");
        }
    }
}
