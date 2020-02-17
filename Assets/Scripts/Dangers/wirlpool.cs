using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wirlpool : MonoBehaviour
{
    BoatMovementV01 boat;
    public float intoWhirlSpeed = 8f;
    public int clicksForRelease = 10;
    public int clicksClicked = 0;
    [Tooltip("Seconds until the click counter reset")]
    public float secLimit = 3f;
    [Tooltip("How many times the clock resets before auto-release")]
    public int autoRelease = 4;
    [Tooltip("How many times the clock has restarted")]
    public int autoReleaseCounter = 0;
    float counter = 0f;
    bool activated = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {
        if (activated)
        {
            MoveBoat();
            CountClicks();
        }
    }

    public void MoveBoat()
    {
        boat.transform.position = Vector3.MoveTowards(boat.transform.position, transform.position, intoWhirlSpeed * Time.deltaTime);

        if (transform.position == boat.transform.position && clicksClicked < clicksForRelease)
        {
            boat.transform.position = transform.position;
        }
        if(clicksClicked >= clicksForRelease)
        {
            activated = false;
        }
    }

    private void CountClicks()
    {
        counter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clicksClicked++;
            Debug.Log("clicks clicked: " + clicksClicked);
        }
        if(counter >= secLimit)
        {
            clicksClicked = 0;
            counter = 0f;
            autoReleaseCounter++;
            Debug.Log("autoReleaseCounter: " + autoReleaseCounter);
        }
        if(autoRelease <= autoReleaseCounter)
        {
            activated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = true;
    }
}
