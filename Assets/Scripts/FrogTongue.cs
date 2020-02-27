﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FrogTongue : MonoBehaviour
{
    public float throwSpeed = .5f;
    public float reelSpeed = .5f;
    [SerializeField] private GameObject tongueTip;
    [SerializeField] private LineRenderer tongueBody;
    [SerializeField] AudioSource launchSound;
    [SerializeField] AudioSource catchSound;
    private Vector2 _targetPos;
    private Vector2 _mousePos;
    private bool _thrownTongue = false;
    private bool _grabbedItem = false;

    private void Start()
    {
        _targetPos = transform.position;
    }

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        if (Input.GetMouseButtonDown(0) && !_thrownTongue)
        {
            _thrownTongue = true;
            SetTargetPosition(_mousePos);
            var newSound = Instantiate(launchSound, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(newSound, 1f);
        }
        
        MoveTongueToTarget();
        CheckForItemAtTip();

        if (_grabbedItem || Vector2.Distance(tongueTip.transform.position, _targetPos) <= .05f)
        {
            _targetPos = transform.position;
        }


        if (Vector2.Distance(transform.position, tongueTip.transform.position) <= .2f)
        {
            _grabbedItem = false;
            _thrownTongue = false;
        }
        
        
        tongueBody.positionCount = 2;
        Vector3[] positions = new[] { transform.position, tongueTip.transform.position };
        tongueBody.SetPositions(positions);
    }

    private void SetTargetPosition(Vector2 newPos)
    {
        _targetPos = newPos;
    }

    private void CheckForItemAtTip()
    {
        foreach (var c in Physics2D.OverlapBoxAll(tongueTip.transform.position, new Vector2(.3f, .3f), 0))
        {
            if (c.GetComponent<Collectable>())
            {
                c.transform.parent = tongueTip.transform;
                var sound = Instantiate(catchSound, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(sound, 1f);

                transform.parent = tongueTip.transform;
                _grabbedItem = true;
                break;

            }
        }
    }
    
    private void MoveTongueToTarget()
    {
        var tongueSpeed = 0f;

        tongueSpeed = _grabbedItem ? reelSpeed : throwSpeed;
        
        tongueTip.transform.position = Vector2.MoveTowards(tongueTip.transform.position, _targetPos, tongueSpeed * Time.deltaTime * 100);
    }
}
