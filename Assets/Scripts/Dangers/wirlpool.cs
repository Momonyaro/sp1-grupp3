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
    public GameObject spacebarSwe;
    public GameObject spacebarEng;
    float whirlCounter = 0f;
    float timeCounter = 0f;
    bool activated = false;

    Translator tr;

    private const float deltaScale = 100;

    Shield shield;
    TextManager tm;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
        shield = FindObjectOfType<Shield>();
        tm = FindObjectOfType<TextManager>();
        activated = false;
        tr = FindObjectOfType<Translator>();
    }

    void Update()
    {
        if (activated)
        {
            MoveBoat();
            Spacebar();
            CountClicks();
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0)
            {
                timeCounter = 1f;
                FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Whirlpool);
            }
        }
    }

    private void Spacebar()
    {
        if (tr.language == Language.English)
        {
            spacebarEng.SetActive(true);
        }
        else
        {
            spacebarSwe.SetActive(true);
        }
    }

    public void MoveBoat()
    {
        boat.transform.position = Vector3.MoveTowards(boat.transform.position, transform.position, intoWhirlSpeed * Time.deltaTime);
        boat.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime * deltaScale));
    }

    private void CountClicks()
    {
        whirlCounter += Time.deltaTime;
        if (Input.anyKeyDown)
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
        if(autoRelease <= autoReleaseCounter && boat.shield)
        {
            StartCoroutine(PushOut());
            activated = false;
            boat.transform.rotation = Quaternion.identity;
            //boat.LostHealth();
            shield.ResetCoinCounter();
            boat.shield = false;
            tm.ResetCoinCount();
            spacebarEng.SetActive(false);
            spacebarSwe.SetActive(false);

            boat.shield = false;
        }
        else if(autoRelease <= autoReleaseCounter && !boat.shield)
        {
            StartCoroutine(PushOut());
            activated = false;
            boat.transform.rotation = Quaternion.identity;
            boat.LostHealth();
            shield.ResetCoinCounter();
            //boat.shield = false;
            tm.ResetCoinCount();
            spacebarEng.SetActive(false);
            spacebarSwe.SetActive(false);

            boat.shield = false;
        }
        if (clicksClicked >= clicksForRelease)
        {
            StartCoroutine(PushOut());
            activated = false;
            spacebarEng.SetActive(false);
            spacebarSwe.SetActive(false);

            boat.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            activated = true;
            FindObjectOfType<BoatTail>().BoatTrail(false);
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
        boat.goldBoat.color = boat.defaultColor;
    }
}
