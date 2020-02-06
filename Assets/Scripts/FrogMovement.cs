using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] float frogSpeedX = 12f;
    [SerializeField] float jumpPower = 12f;
    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    string horizontal = "Horizontal";
    float x;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Walking();
        Jump();
    }

    private void Walking()
    {
        x = Input.GetAxis(horizontal) * Time.deltaTime * frogSpeedX;
        float newPosX = transform.position.x + x;
        transform.position = new Vector2(newPosX, transform.position.y);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}