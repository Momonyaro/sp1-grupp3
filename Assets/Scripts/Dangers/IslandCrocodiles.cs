using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCrocodiles : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject path = null;
    Hit hit;
    int currentWaypoint = 0;

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
        Move();
        //Rotating();
    }

    private void Move()
    {
        if (currentWaypoint <= waypoints.Count - 1)
        {
            var targetPos = waypoints[currentWaypoint].transform.position;
            var moving = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moving);
            if (transform.position == targetPos)
            {
                currentWaypoint++;
            }
        }
        else
        {
            currentWaypoint = 0;
        }
    }
    //private void Rotating() //https://www.youtube.com/watch?v=mKLp-2iseDc
    //{
    //    Vector2 direction = target.position - transform.position;
    //    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    //    Quaternion rot = Quaternion.AngleAxis(angle)
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hit.KnockingBack();
            Debug.Log("Hit islandcroc");
        }
    }
}
