using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BoatMovementV01 : MonoBehaviour
{
    public float autoSpeed = 3.0f;
    public float tiltSpeed = 5.0f;
    public float breakSpeed = 1.0f;
    public float rowSpeed = 8.0f;
    public float rowPower = 500.0f;
    [SerializeField] float currentSpeedY = 0;
    [Tooltip("How long after a collision the frog will be immortal (in seconds)")]
    public float immortalTime = 1f;
    bool knockback = false;
    bool stunned = false;
    bool shield = false;
    bool stopBoat = false;

    public Color hurtColor = Color.red;
    public Color defaultColor = Color.green;
    public Color deadColor = Color.gray;

    bool gotHit = false;
    float counter = 0f;
    [Tooltip("Only between 1 and 5 so far. Consult Teo for upgrades")]
    [Range(1, 5)]
    public int maxHealth = 3;
    public static int currentHealth;
    public SignalThingy playerHealthSignal;
    public SpriteRenderer headRenderer;
    Hit hit;

    public bool GameOver = false;

    Rigidbody2D rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        hit = FindObjectOfType<Hit>();
        currentHealth = maxHealth;
        //Time.timeScale = 1;
    }

    void Update()
    {
        if (!GameOver && knockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            currentSpeedY = rigidb.velocity.y;

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && stunned == false)
            {
                FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Brake);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && stunned == false)
            {
                FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Dash);
            }

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

        if (currentHealth <= 0)
        {
            GameOver = true;
        }
        else
        {
            GameOver = false;
        }

        if (GameOver)
        {
            TextManager.gameOver = true;
            autoSpeed = 0;
        }
        else
        {
            TextManager.gameOver = false;
        }
        if (stopBoat)
        {
            autoSpeed = 0;
        }
        if (gotHit)
        {
            counter += Time.deltaTime;
            headRenderer.color = hurtColor;
            GetComponent<SpriteRenderer>().color = hurtColor;
        }
        if (counter > immortalTime)
        {
            gotHit = false;
            counter = 0f;
            headRenderer.color = defaultColor;
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
        if (currentHealth <= 0)
        {
            headRenderer.color = deadColor;
            GetComponent<SpriteRenderer>().color = deadColor;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dangerous" && shield == false && gotHit == false)
        {
            gotHit = true;
            playerHealthSignal.Raise();
            currentHealth -= 1;
            Debug.Log("Lost health. Current health:" + currentHealth);

        }
        if (other.tag == "Dangerous" && shield && gotHit == false)
        {
            shield = false;
            hit.ShieldSwitchBool();
        }
    }
}
