using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] GameObject leftPoint = null;
    [SerializeField] GameObject rightPoint = null;
    [SerializeField] float speed = 2f;
    [Tooltip("Decides direction of clouds")]
    public bool direction = false;

    void Start()
    {
        if (direction)
        {
            transform.position = leftPoint.transform.position;
        }
        else
        {
            transform.position = rightPoint.transform.position;
        }
    }

    void Update()
    {
        MoveCloud();
    }

    private void MoveCloud()
    {
        if (direction)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.transform.position, speed * Time.deltaTime);
            if(transform.position.x == rightPoint.transform.position.x)
            {
                transform.position = leftPoint.transform.position;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.transform.position, speed * Time.deltaTime);
            if (transform.position.x == leftPoint.transform.position.x)
            {
                transform.position = rightPoint.transform.position;
            }
        }

        
    }
}
