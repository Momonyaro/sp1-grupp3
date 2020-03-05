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
    public float knockbackTime = .5f;
    [SerializeField] float knockbackPower = 300f;
    bool knockback = false;
    bool stunned = false;
    bool shield = false;
    bool stopBoat = false;

    public Color hurtColor = Color.red;
    public Color defaultColor = Color.green;
    public Color deadColor = Color.gray;

    bool gotHit = false;
    float counter = 0f;
    float timer = .5f;
    [Tooltip("Only between 1 and 5 so far. Consult Teo for upgrades")]
    [Range(1, 5)]
    public int maxHealth = 3;
    public static int currentHealth;
    public SignalThingy playerHealthSignal;
    public SpriteRenderer headRenderer;
    Hit hit;
    Shaker shaker;
    private int freezeFrames = 0;

    public bool GameOver = false;

    Rigidbody2D rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        hit = FindObjectOfType<Hit>();
        shaker = FindObjectOfType<Shaker>();
        currentHealth = maxHealth;
        //GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (freezeFrames > 0)
        {
            Time.timeScale = 0;
            freezeFrames--;
        }
        else if (freezeFrames == 0)
        {
            Time.timeScale = 1;
            freezeFrames--;
        }
        
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
            GetComponent<Collider2D>().enabled = false;
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

        timer -= Time.deltaTime;
    }

    public void StopBoatSwitchBool()
    {
        stopBoat = !stopBoat;
    }

    public void SetKnockbackBool(bool set)
    {
        knockback = set;
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

    public void LostHealth()
    {
        currentHealth--;
        Debug.Log("Lost health. Current health:" + currentHealth);
        InsertFreezeFrames(6);
        FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.BoatCrash);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dangerous" && shield == false && gotHit == false)
        {
            gotHit = true;
            playerHealthSignal.Raise();
            LostHealth();

        }
        if (other.tag == "Dangerous" && shield && gotHit == false)
        {
            gotHit = true;
            shield = false;
            hit.ShieldSwitchBool();
            KnockbackDangers(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Knockback(collision);
    }

    public void Knockback(Collision2D danger)
    {
        if (timer < 0)
        {
            timer = .3f;
            StartCoroutine(shaker.Shake());
            var newDistance = GetComponent<Rigidbody2D>().transform.position - danger.transform.position;
            knockback = true;
            StartCoroutine(AccurateKnockback(newDistance));

        }
    }

    public void KnockbackDangers(Collider2D danger)
    {
        if (timer < 0)
        {
            timer = 1f;
            StartCoroutine(shaker.Shake());
            var newDistance = GetComponent<Rigidbody2D>().transform.position - danger.transform.position;
            knockback = true;
            StartCoroutine(AccurateKnockback(newDistance));

        }
    }

    IEnumerator AccurateKnockback(Vector3 newDistance)
    {
        GetComponent<Rigidbody2D>().AddForce(newDistance.normalized * knockbackPower);
        yield return new WaitForSeconds(knockbackTime);
        knockback = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void InsertFreezeFrames(int amount)
    {
        Debug.Log("[Za Warudo] Froze time for " + amount + " frames.");
        freezeFrames = amount;
    }
}
