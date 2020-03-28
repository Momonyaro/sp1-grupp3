using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacebar : MonoBehaviour
{
    [SerializeField] GameObject eng;
    [SerializeField] GameObject swe;
    public bool english = false;
    bool active = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (active)
        {
            if (english)
            {
                eng.SetActive(true);
                active = false;
            }
            else
            {
                swe.SetActive(true);
                active = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter the vhurl");
        active = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit the vhurl");
        swe.SetActive(false);
        eng.SetActive(false);
    }
}
