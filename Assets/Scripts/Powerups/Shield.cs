using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    PowerupController pwrctr;
    // Start is called before the first frame update
    void Start()
    {
        pwrctr = FindObjectOfType<PowerupController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pwrctr.ShieldActivation(collision);
        Destroy(gameObject);
    }
}
