using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementV01 : MonoBehaviour
{
    public float autoSpeed = 3.0f;
    public float tiltSpeed = 5.0f;
    public float breakSpeed = 1.0f;
    public float rowSpeed = 8.0f;
    public float rowPower = 500.0f;
    public float intoWhirlSpeed = 10f;
    [SerializeField] float currentSpeedY = 0;
    bool knockback = false;
    bool stunned = false;
    bool shield = false;
    bool stopBoat = false;

    public static int maxHealth = 3;
    [SerializeField] int currentHealth;

    public bool GameOver = false;

    Rigidbody2D rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        //Time.timeScale = 1;
    }

    void Update()
    {
        if (!GameOver && knockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            currentSpeedY = rigidb.velocity.y;

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && stunned == false)
            {
                autoSpeed = breakSpeed;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && stunned == false)
            {
                autoSpeed = rowSpeed;
            }
            else
            {
                autoSpeed = 3.0f;
            }

            Vector2 position = transform.position;

            if(stunned)
            {
                position.x = position.x + tiltSpeed * -horizontal * Time.deltaTime;
            }
            else
            {
                position.x = position.x + tiltSpeed * horizontal * Time.deltaTime;
            }

            position.y = position.y + 1.0f * autoSpeed * Time.deltaTime;

            rigidb.MovePosition(position);
        }

        if (GameOver)
        {
            autoSpeed = 0;
        }
        if (stopBoat)
        {
            autoSpeed = 0;
        }
    }

    public void StopBoatSwitchBool()
    {
        stopBoat = !stopBoat;
    }

    public void KnockbackBoolSwitch()
    {
        knockback = !knockback;
    }

    public void ShieldBoolTrue()
    {
        shield = true;
    }

    public bool StunStatus()
    {
        return stunned;
    }
    public void StunnedBoolSwitch()
    {
        stunned = !stunned;
        Debug.Log("Stunned bool: " + stunned);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dangerous" && shield == false) //+ timer så man ej kan ta skada när man knockas tillbaka?
        {
            currentHealth -= 1;
            Debug.Log("Lost health. Current health:" + currentHealth);
            TextManager.health -= 1;

            if(currentHealth <= 0)
            {
                GameOver = true;
                TextManager.gameOver = true;
            }
        }
        if (shield)
        {
            shield = false;
        }
    }
}
