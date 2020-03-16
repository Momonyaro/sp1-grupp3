using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class wirlpool : MonoBehaviour
{
    BoatMovementV01 boat;
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
    float whirlCounter = 0f;
    float timeCounter = 0f;
    bool activated = false;
    //public GameObject leftPoint = null;
    //public GameObject rightPoint = null;
    bool direction = false;

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
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0)
            {
                timeCounter = 1f;
                FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Whirlpool);
            }
        }
    }

    IEnumerator Sound()
    {
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Whirlpool);
        yield return new WaitForSeconds(1f);
    }
    public void MoveBoat()
    {
        //FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Whirlpool);

        boat.transform.position = Vector3.MoveTowards(boat.transform.position, transform.position, intoWhirlSpeed * Time.deltaTime);
        boat.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        //RotatingBoat();
    }

    //private void RotatingBoat()
    //{
        //if(leftPoint.transform.position == boat.transform.position)
        //{
        //    direction = false;
        //}
        //else if(rightPoint.transform.position == boat.transform.position)
        //{
        //    direction = true;
        //}

        //if (direction)
        //{
        //    boat.transform.position = Vector3.MoveTowards(boat.transform.position, leftPoint.transform.position, insideWhirlSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    boat.transform.position = Vector3.MoveTowards(boat.transform.position, rightPoint.transform.position, insideWhirlSpeed * Time.deltaTime);
        //}
    //}

    private void CountClicks()
    {
        //FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Whirlpool);

        whirlCounter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clicksClicked++;
            Debug.Log("clicks clicked: " + clicksClicked);
        }
        if(whirlCounter >= secLimit)
        {
            clicksClicked = 0;
            whirlCounter = 0f;
            autoReleaseCounter++;
            Debug.Log("autoReleaseCounter: " + autoReleaseCounter + " / " + autoRelease);
        }
        if(autoRelease <= autoReleaseCounter)
        {
            StartCoroutine(PushOut());
            activated = false;
            boat.transform.rotation = Quaternion.identity;
            boat.LostHealth();
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
            boat.knockback = true;
        }
    }

    IEnumerator PushOut()
    {
        boat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, knockForwardPower));
        yield return new WaitForSeconds(knockForwardTime);
        boat.knockback = false;
        boat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        boat.GetComponent<SpriteRenderer>().color = boat.defaultColor;
        boat.headRenderer.color = boat.defaultColor;
    }
}
