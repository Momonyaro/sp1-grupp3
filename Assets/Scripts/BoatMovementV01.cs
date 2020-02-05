using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementV01 : MonoBehaviour
{
    public float autoSpeed = 3.0f;
    public float tiltSpeed = 5.0f;
    public float breakSpeed = 1.0f;
    public float rowSpeed = 8.0f;
    //public float rowTime = 2.0f;
    public float rowPower = 500.0f;
    [SerializeField] float currentSpeedY = 0;

    Rigidbody2D rigidb;
    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        currentSpeedY = rigidb.velocity.y;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            autoSpeed = breakSpeed;
            Debug.Log("S i pressed");
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //StartCoroutine(StartCounting(rowTime));
            //rigidb.AddForce(new Vector2(0, rowPower), ForceMode2D.Force);
            //rigidb.MovePosition(Vector2.up * rowPower);
            autoSpeed = rowSpeed;
            Debug.Log("W is pressed");
        }
        else
        {
            autoSpeed = 3.0f;
        }

        Vector2 position = transform.position;
        position.x = position.x + tiltSpeed * horizontal * Time.deltaTime;

        position.y = position.y + 1.0f * autoSpeed * Time.deltaTime;

        rigidb.MovePosition(position);
    }

    /*public IEnumerator StartCounting(float rowTime)
    {
        Debug.Log("Counting down");
        yield return new WaitForSeconds(rowTime);
    }*/
}
