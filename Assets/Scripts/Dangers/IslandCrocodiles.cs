using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCrocodiles : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 5f;
    Hit hit;
    int currentWaypoint = 0;

    void Start()
    {
        transform.position = waypoints[currentWaypoint].transform.position;
        hit = FindObjectOfType<Hit>();
    }

    void Update()
    {
        Move();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit.CrocHit(collision);
    }
}
