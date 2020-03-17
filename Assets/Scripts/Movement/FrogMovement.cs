using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] float frogSpeedX = 12f;
    string horizontal = "Horizontal";
    public static bool frozen = false;
    float x;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        Time.timeScale = 1;
        frozen = false;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!frozen)
            Move();

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > float.Epsilon)
        {
            if (Input.GetAxis("Horizontal") > 0.0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            // bas synvinkeln
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > float.Epsilon)
            {
                if (Input.GetAxis("Horizontal") < 0.0)
                {

                }
                else
                {

                }
            }
        }
    }

    private void Move()
    {
        x = Input.GetAxis(horizontal) * Time.deltaTime * frogSpeedX;
        float newPosX = transform.position.x + x;
        transform.position = new Vector2(newPosX, transform.position.y);
       
    }
}