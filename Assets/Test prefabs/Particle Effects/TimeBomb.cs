using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    public float timeToDestroy = 1;

    private float timer;

    private void Awake()
    {
        timer = timeToDestroy;
    }

    private void Update()
    {
        if(timer < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
