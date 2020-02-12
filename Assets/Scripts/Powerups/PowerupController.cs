using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    BoatMovementV01 boat;
    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {
        
    }

    public void ShieldActivation(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boat.ShieldBoolTrue();
        }
    }
}
