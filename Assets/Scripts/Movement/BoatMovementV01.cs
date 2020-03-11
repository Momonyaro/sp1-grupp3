﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BoatMovementV01 : MonoBehaviour
{
    public float defaultAutoSpeed = 3.0f;
    public float tiltSpeed = 5.0f;
    public float breakSpeed = 1.0f;
    public float rowSpeed = 8.0f;
    [Tooltip("How long after a collision the frog will be immortal (in seconds)")]
    public float immortalTime = 1f;
    public float knockbackTime = .5f;
    [SerializeField] float knockbackPower = 300f;
    public bool knockback = false;
    public bool stunned = false;
    public bool shield = false;
    public bool stopBoat = false;
    public int freezeFrames = 6;

    public Color hurtColor = Color.red;
    public Color defaultColor = Color.green;
    public Color deadColor = Color.gray;

    public GameObject hurtEffect;
    private bool _gotHit = false;
    private float _counter = 0f;
    private float _timer = .5f;
    private float _autoSpeed;
    [Tooltip("Only between 1 and 5 so far. Consult Teo for upgrades")]
    [Range(1, 5)]
    public int maxHealth = 3;
    public static int currentHealth;
    public SignalThingy playerHealthSignal;
    public SpriteRenderer headRenderer;
    public float pushbackPower = 2f;
    private Hit _hit;
    private Shaker _shaker;
    private int _freezeFrames = 0;
    private const float DangerKnockbackTimer = 1;
    [Space]
    [Tooltip("Vilken riktning tile-knockbacken pushar grodan")]
    //public bool knockbackDirection = false;
    private Vector3 _oldVelocity;
    private Vector3 _oldPosition;
    [SerializeField] float originOffsetX = 0.5f;
    [SerializeField] float originOffsetY = 0.7f;

    public bool GameOver = false;

    private bool _pressedS = false;
    private bool _pressedW = false;

    Rigidbody2D rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        _hit = FindObjectOfType<Hit>();
        _shaker = FindObjectOfType<Shaker>();
        currentHealth = maxHealth;
        _oldVelocity = rigidb.velocity;
        _oldPosition = transform.position;
        //GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (_freezeFrames > 0)
        {
            Time.timeScale = 0;
            _freezeFrames--;
        }
        else if (_freezeFrames == 0)
        {
            Time.timeScale = 1;
            _freezeFrames--;
        }
        
        if (!GameOver && !knockback)
        {
            float horizontal = Input.GetAxis("Horizontal");

            _autoSpeed = stopBoat || GameOver ? 0 : defaultAutoSpeed;
            
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && !stunned)
            {
                if (!_pressedS)
                {
                    _pressedS = true;
                    FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Brake);
                }
                else
                    _pressedS = false;
                
                _autoSpeed = breakSpeed;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && !stunned)
            {
                if (!_pressedW)
                {
                    _pressedW = true;
                    FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Dash);
                }
                else
                    _pressedW = false;
                
                _autoSpeed = rowSpeed;
            }

            Vector2 position = transform.position;
            position.y += + 1.0f * _autoSpeed * Time.deltaTime;
            position.x += + tiltSpeed * horizontal * Time.deltaTime;

            if(stunned)
            {
                position.x += + tiltSpeed * -horizontal * Time.deltaTime;
            }

            rigidb.MovePosition(position);
        }

        GameOver = currentHealth <= 0;
        TextManager.gameOver = GameOver;

        if (GameOver)
        {
            headRenderer.color = deadColor;
            GetComponent<SpriteRenderer>().color = deadColor;
            GetComponent<Collider2D>().enabled = false;
        }

        if (_counter > immortalTime)
        {
            _gotHit = false;
            _counter = 0f;
            headRenderer.color = defaultColor;
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
        else if (_gotHit)
        {
            _counter += Time.deltaTime;
        }

        _timer -= Time.deltaTime;

        _oldVelocity = (transform.position - _oldPosition) * 100;
        _oldPosition = transform.position;
    }

    public void LostHealth()
    {
        currentHealth--;
        Debug.Log("Lost health. Current health:" + currentHealth);
        GetComponent<SpriteRenderer>().color = hurtColor;
        headRenderer.color = hurtColor;
        if(hurtEffect != null)
        {
            Instantiate(hurtEffect, transform.position, Quaternion.identity);
        }
        InsertFreezeFrames(freezeFrames);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dangerous") && !_gotHit)
        {
            if (!shield)
            {
                playerHealthSignal.Raise();
                _gotHit = true;
                LostHealth();
            }
            else
            {
                shield = false;
                _gotHit = true;
                _hit.ShieldSwitchBool();
                KnockbackDangers(other);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, -originOffsetX))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 10, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

        }

        //Knockback(collision);
        //if (collision.transform.CompareTag("Tilemap"))
        //{
        //    //knockbackTime -= Time.deltaTime;
        //    Debug.Log("Knocked into the tilemap");
        //    if (collision.transform.position.x <= transform.position.x)
        //    {
        //        Knockback(false);
        //        //    StartCoroutine(TilemapKnockback(true));
        //        //    //rigidb.AddForce(new Vector2(pushbackPower, -pushbackPower));
        //        //    //rigidb.velocity = new Vector2(-pushbackPower, -pushbackPower);
        //    }
        //    else //if(collision.transform.position.x > transform.position.x)
        //    {
        //        Knockback(true);
        //        //    StartCoroutine(TilemapKnockback(false));

        //        //    //rigidb.AddForce(new Vector2(pushbackPower, -pushbackPower));
        //        //    //rigidb.velocity = new Vector2(pushbackPower, -pushbackPower);
        //    }
        //}

    }

    //IEnumerator TilemapKnockback(bool direction)
    //{
    //    stopBoat = true;
    //    if (direction)
    //    {
    //        rigidb.velocity = new Vector2(-pushbackPower, -transform.position.y);
    //    }
    //    else
    //    {
    //        rigidb.velocity = new Vector2(pushbackPower, transform.position.y);
    //    }
    //    yield return new WaitForSeconds(.5f);
    //    stopBoat = false;
    //}

    public void Knockback(bool direction)
    {
        if (_timer < 0)
        {
            knockback = true;
            Vector3 vel = _oldVelocity;
            vel = vel * -1;
            StartCoroutine(TilemapKnockback(vel));
            //var newDistance = GetComponent<Rigidbody2D>().transform.position - danger.transform.position;
            //StartCoroutine(AccurateKnockback(newDistance));
            StartCoroutine(_shaker.Shake());

            _timer = .3f;
        }
    }

    IEnumerator TilemapKnockback(Vector3 velocity)
    {
        velocity.Normalize();
        Debug.Log("rigidb velocity = " + rigidb.velocity + " | inverted velocity = " + velocity);
        rigidb.AddForce(velocity * pushbackPower);
        //rigidb.velocity = velocity * pushbackPower;
        yield return new WaitForSeconds(.1f);
        knockback = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }


    public void KnockbackDangers(Collider2D danger)
    {
        if (_timer < 0)
        {
            knockback = true;
            var newDistance = GetComponent<Rigidbody2D>().transform.position - danger.transform.position;
            StartCoroutine(AccurateKnockback(newDistance));
            StartCoroutine(_shaker.Shake());

            _timer = DangerKnockbackTimer;
        }
    }

    public IEnumerator AccurateKnockback(Vector3 newDistance)
    {
        GetComponent<Rigidbody2D>().AddForce(newDistance.normalized * knockbackPower);
        
        //Vänta lite innan man nollar hastigheten.
        yield return new WaitForSeconds(knockbackTime);
        
        knockback = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void InsertFreezeFrames(int amount)
    {
        Debug.Log("[Za Warudo] Froze time for " + amount + " frames.");
        _freezeFrames = amount;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawLine(new Vector2(transform.position.x + originOffsetX, transform.position.y), new Vector2(transform.position.x - originOffsetX, transform.position.y));
    //    Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + originOffsetY), new Vector2(transform.position.x, transform.position.y - originOffsetY));
    //}
}