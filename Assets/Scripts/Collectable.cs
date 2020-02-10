using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    CollectableHandeler ch;
    // Start is called before the first frame update
    void Start()
    {
        ch = GetComponent<CollectableHandeler>();
        ch.setDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ch.pickedUp();
            Destroy(this);
        }
    }
}
