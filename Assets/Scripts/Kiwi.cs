using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi : MonoBehaviour
{
    public float moveSpeed = 5;
    int direction = -1;
    public ParticleSystem scream;

    Rigidbody2D rigidb;

    private void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 position = rigidb.position;

        position.x += direction * moveSpeed * Time.deltaTime;

        rigidb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            direction = direction * -1;

            if(direction == 1)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            else
                transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
