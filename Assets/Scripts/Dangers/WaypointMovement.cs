using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject path = null;
    //[SerializeField] AudioClip biteClipSound = null;
    int currentWaypoint = 0;
    int lastWaypoint;
    //float timer = 0f;
    bool rotating = false;

    void Start()
    {
        GetWaypoints();
        transform.position = waypoints[currentWaypoint].transform.position;
    }

    public void GetWaypoints()
    {
        foreach (Transform child in path.transform)
        {
            waypoints.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
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
            Destroy(gameObject);
        }
    }

    public void Rotation()
    {
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
