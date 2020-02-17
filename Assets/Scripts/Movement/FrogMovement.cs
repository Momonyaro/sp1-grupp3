using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] float frogSpeedX = 12f;
    string horizontal = "Horizontal";
    float x;
    

    void Update()
    {
        Move();
    }

    private void Move()
    {
        x = Input.GetAxis(horizontal) * Time.deltaTime * frogSpeedX;
        float newPosX = transform.position.x + x;
        transform.position = new Vector2(newPosX, transform.position.y);
    }
}