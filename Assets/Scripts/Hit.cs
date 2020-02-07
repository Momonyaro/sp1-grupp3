using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    bool croc = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrocHit(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            croc = true;
            Debug.Log("Player hit by croc");
        }
    }
}
