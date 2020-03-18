﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using System;

public class BoatMovementV01 : MonoBehaviour
{
    public float defaultAutoSpeed = 3.0f;
    public float tiltSpeed = 5.0f;
    public float breakSpeed = 1.0f;
    public float rowSpeed = 8.0f;
    [Tooltip("How long after a collision the frog will be immortal (in seconds)")]
    public float immortalTime = 1.2f;
    public float knockbackTime = .5f;
    [SerializeField] float knockbackPower = 300f;
    public bool knockback = false;
    public bool stunned = false;
    public bool shield = false;
    public bool stopBoat = false;
    public int freezeFrames = 6;
    int collidingInto = 0;
    bool collided = false;

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
    private Vector3 _oldVelocity;
    private Vector3 _oldPosition;

    public bool GameOver = false;

    private bool _pressedS = false;
    private bool _pressedW = false;

    Rigidbody2D rigidb;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        _hit = FindObjectOfType<Hit>();
        _shaker = FindObjectOfType<Shaker>();
        currentHealth = maxHealth;
        _oldVelocity = rigidb.velocity;
        _oldPosition = transform.position;
        _autoSpeed = defaultAutoSpeed;
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
                
                _autoSpeed = breakSpeed;
            }
            else
                _pressedS = false;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && !stunned)
            {
                if (!_pressedW)
                {
                    _pressedW = true;
                    FindObjectOfType<AudioManager>().requestSoundDelegate(Sounds.Dash);
                }
                
                _autoSpeed = rowSpeed;
            }
            else
                _pressedW = false;

            Vector2 position = transform.position;
            position.y += 1.0f * _autoSpeed * Time.deltaTime;
            position.x += tiltSpeed * horizontal * Time.deltaTime;

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
                Shield.coinCount = 0;
                _hit.ShieldSwitchBool();
                KnockbackDangers(other);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint[] points = new ContactPoint[4];

        //for (int i = 0; i < points.Length; i++)
        //{
        //    if(Vector2.Distance(points[i].point, upRight.position) < 6f)
        //    {
        //        collidingInto = 1;
        //        collided = true;
        //        //StartCoroutine(_shaker.Shake());

        //        Debug.Log("Up Right Hit");
        //    }
        //    else if(Vector2.Distance(points[i].point, upLeft.position) < 6f)
        //    {
        //        collidingInto = 2;
        //        collided = true;
        //        //StartCoroutine(_shaker.Shake());

        //        Debug.Log("Up Left Hit");
        //    }
        //    else if(Vector2.Distance(points[i].point, downRight.position) < 6f)
        //    {
        //        collidingInto = 3;
        //        collided = true;
        //        //StartCoroutine(Knockbacking(3));
        //        //StartCoroutine(_shaker.Shake());

        //        Debug.Log("Down Right Hit");
        //    }
        //    else if(Vector2.Distance(points[i].point, downLeft.position) < 6f)
        //    {
        //        collidingInto = 4;
        //        collided = true;
        //        //knockback = true;
        //        //StartCoroutine(Knockbacking(4));
        //        //StartCoroutine(_shaker.Shake());

        //        Debug.Log("Down Left Hit");
        //    }
        //}

    }

    //void HandelingKnockback()
    //{
    //    if (collided /*&& _timer < 0*/)
    //    {
    //        Debug.Log("collided = " + collided);
    //        if(collidingInto == 1)
    //        {
    //            Debug.Log("collidingInfo = " + collidingInto);
    //            collided = false;
    //            //StartCoroutine(Knockbacking(1));
    //        }
    //        else if(collidingInto == 2)
    //        {
    //            Debug.Log("collidingInfo = " + collidingInto);
    //            collided = false;
    //            //StartCoroutine(Knockbacking(1));
    //        }
    //        else if(collidingInto == 3)
    //        {
    //            Debug.Log("collidingInfo = " + collidingInto);
    //            collided = false;
    //            //StartCoroutine(Knockbacking(1));
    //        }
    //        else if(collidingInto == 4)
    //        {
    //            Debug.Log("collidingInfo = " + collidingInto);
    //            collided = false;
    //            //StartCoroutine(Knockbacking(1));
    //        }
    //    }
    //}

    //IEnumerator Knockbacking(int i)
    //{
    //    if (_timer < 0)
    //    {
    //        knockback = true;
    //        Vector3 vel = _oldVelocity;
    //        vel = vel * -1;
    //        StartCoroutine(TilemapKnockback(vel));
    //        StartCoroutine(_shaker.Shake());

    //        _timer = .3f;
    //    }

    //}

    public void Knockback(bool direction)
    {
        if (_timer < 0)
        {
            knockback = true;
            Vector3 vel = _oldVelocity;
            vel = vel * -1;
            StartCoroutine(TilemapKnockback(vel));
            StartCoroutine(_shaker.Shake());

            _timer = .3f;
        }
    }

    IEnumerator TilemapKnockback(Vector3 velocity)
    {
        velocity.Normalize();
        Debug.Log("rigidb velocity = " + rigidb.velocity + " | inverted velocity = " + velocity);
        rigidb.AddForce(velocity * pushbackPower);
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
}