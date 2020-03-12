using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCrocodiles : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject path = null;
    //[SerializeField] AudioClip biteClipSound = null;
    BoatMovementV01 boat;
    int currentWaypoint = 0;
    int lastWaypoint;
    float timer = 0f;
    bool rotating = false;
    public GameObject target;
    public float distance;

    void Start()
    {
        GetWaypoints();
        if (waypoints.Count > 0)
        {
            transform.position = waypoints[currentWaypoint].transform.position;
        }
        boat = FindObjectOfType<BoatMovementV01>();
    }

    public void GetWaypoints()
    {
        if (path != null)
        {
            foreach (Transform child in path.transform)
            {
                waypoints.Add(child);
            }
        }
    }

    void Update()
    {
        if(target != null)
            distance = Vector3.Distance(target.transform.position, transform.position);
        

        timer -= Time.deltaTime;
        
        Move();
    }

    private void Move()
    {
        if (waypoints.Count > 0)
        {
            if (currentWaypoint <= waypoints.Count - 1)
            {
                var targetPos = waypoints[currentWaypoint].transform.position;
                var moving = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moving);

                if (transform.position == targetPos)
                {
                    lastWaypoint = currentWaypoint;
                    currentWaypoint++;
                    //Debug.Log("Last waypoint: " + lastWaypoint + ". Current waypoint: " + currentWaypoint + ".");
                }
            }
            else
            {
                lastWaypoint = currentWaypoint;
                currentWaypoint = 0;
                //Debug.Log("Last waypoint: " + lastWaypoint + ". Current waypoint: " + currentWaypoint + ".");
            }

            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }

            if (waypoints[lastWaypoint].transform.position.x < waypoints[currentWaypoint].transform.position.x)
            {
                rotating = true;
            }
            else if (waypoints[lastWaypoint].transform.position.x > waypoints[currentWaypoint].transform.position.x)
            {
                rotating = false;
            }
            GetComponent<SpriteRenderer>().flipX = rotating;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && timer <= 0)
        {
            boat.KnockbackDangers(GetComponent<Collider2D>());
            FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.CrocodileBite);

            //AudioSource.PlayClipAtPoint(biteClipSound, new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Debug.Log("Hit islandcroc");
            timer = 1f;
        }
    }
}
