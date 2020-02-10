using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    BoatMovementV01 boat;
    bool croc = false;
    string player = "Player";

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
