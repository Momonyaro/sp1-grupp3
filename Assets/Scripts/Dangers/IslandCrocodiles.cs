using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCrocodiles : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject path = null;
    [SerializeField] AudioClip biteClipSound = null;
    Hit hit;
    int currentWaypoint = 0;
    int lastWaypoint;
    float timer = 0f;
    bool rotating = false;
    bool wayp = false;

    void Start()
    {
        GetWaypoints();
        transform.position = waypoints[currentWaypoint].transform.position;
        hit = FindObjectOfType<Hit>();
    }

    public void GetWaypoints()
    {
        foreach (Transform child in path.transform)
        {
            waypoints.Add(child);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        Move();
    }

    private void Move()
    {
        if (currentWaypoint <= waypoints.Count - 1)
        {
            var targetPos = waypoints[currentWaypoint].transform.position;
            var moving = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moving);
            HandleWaypoints(targetPos);
            HandleRotation();
        }
        else
        {
            currentWaypoint = 0;
            lastWaypoint = waypoints.Count;
            Debug.Log("Last waypoint: " + lastWaypoint + ". Current waypoint: " + currentWaypoint + ".");
        }
    }

    private void HandleWaypoints(Vector3 targetPos)
    {
        if (transform.position == targetPos)
        {
            if (lastWaypoint < 0 && wayp == false)
            {
                lastWaypoint = waypoints.Count;
                wayp = true;
            }
            else if (lastWaypoint == 6 && wayp)
            {
                lastWaypoint = 0;
                wayp = false;
            }
            else
            {
                lastWaypoint = currentWaypoint;
                currentWaypoint++;
            }

            Debug.Log("Last waypoint: " + lastWaypoint + ". Current waypoint: " + currentWaypoint + ".");
        }
    }

    private void HandleRotation()
    {
        if (waypoints[lastWaypoint].transform.position.x < waypoints[currentWaypoint].transform.position.x && rotating == false)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            rotating = true;
        }
        else if (waypoints[lastWaypoint].transform.position.x > waypoints[currentWaypoint].transform.position.x && rotating)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotating = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && timer <= 0)
        {
            hit.KnockingBack();
            AudioSource.PlayClipAtPoint(biteClipSound, new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Debug.Log("Hit islandcroc");
            timer = 1f;
        }
    }
}
