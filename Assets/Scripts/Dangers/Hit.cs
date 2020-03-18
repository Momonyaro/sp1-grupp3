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
            shieldSprite.transform.position = boat.transform.position;
        }
    }

    public void ShieldSwitchBool(bool shieldOn)
    {
        shield = shieldOn;
        Debug.Log(shieldSprite.name);
        shieldSprite.SetActive(shield);
    }
}
