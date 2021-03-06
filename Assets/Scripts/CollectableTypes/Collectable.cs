﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip collectSound;
    public int collectableScore = 1;
    [Tooltip("Use a negative number to rotate the other way")]
    public float rotationSpeed = 0;

    [Space]
    public bool holy = false;
    public GameObject shine;
    public float shineRotationSpeed = 70f;

    public GameObject pickupEffect;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
        if (holy)
        {
            shine.SetActive(true);
            shine.transform.Rotate(new Vector3(0, 0, 1) * shineRotationSpeed * Time.deltaTime);
        }
    }
}