using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    BoatMovementV01 boat;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boat.ShieldBoolTrue();
            Destroy(gameObject);

        }
    }
}
