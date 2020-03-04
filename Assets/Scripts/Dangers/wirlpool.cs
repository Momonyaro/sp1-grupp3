using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wirlpool : MonoBehaviour
{
    BoatMovementV01 boat;
    Hit hit;
    public float intoWhirlSpeed = 8f;
    public float knockForwardTime = 1f;
    public float knockForwardPower = 100f;
    public float rotateSpeed = 10;
    public float insideWhirlSpeed = 4f;
    [Space]
    public int clicksForRelease = 10;
    public int clicksClicked = 0;
    [Tooltip("Seconds until the click counter reset")]
    public float secLimit = 3f;
    [Tooltip("How many times the clock resets before auto-release")]
    public int autoRelease = 4;
    [Tooltip("How many times the clock has reset")]
    public int autoReleaseCounter = 0;
    float counter = 0f;
    bool activated = false;
    public GameObject leftPoint = null;
    public GameObject rightPoint = null;
    bool direction = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        hit = FindObjectOfType<Hit>();
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
        //boat.transform.position = Vector3.MoveTowards(boat.transform.position, transform.position, intoWhirlSpeed * Time.deltaTime);
        boat.transform.Rotate(new Vector3(0, 0, rotateSpeed));
        RotatingBoat();
    }

    private void RotatingBoat()
    {
        if(leftPoint.transform.position == boat.transform.position)
        {
            direction = false;
        }
        else if(rightPoint.transform.position == boat.transform.position)
        {
            direction = true;
        }

        if (direction)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, leftPoint.transform.position, insideWhirlSpeed * Time.deltaTime);
        }
        else
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, rightPoint.transform.position, insideWhirlSpeed * Time.deltaTime);
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
            Debug.Log("autoReleaseCounter: " + autoReleaseCounter + " / " + autoRelease);
        }
        if(autoRelease <= autoReleaseCounter)
        {
            StartCoroutine(PushOut());
            activated = false;
            boat.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (clicksClicked >= clicksForRelease)
        {
            StartCoroutine(PushOut());
            activated = false;
            boat.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            activated = true;
            boat.SetKnockbackBool(true);
        }
    }

    IEnumerator PushOut()
    {
        boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, knockForwardPower));
        yield return new WaitForSeconds(knockForwardTime);
        boat.SetKnockbackBool(false);
        boat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
