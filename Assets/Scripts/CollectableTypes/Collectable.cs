using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioSource collectSound;
    public int collectableScore = 1;
    [Tooltip("Use a negative number to rotate the other way")]
    public float rotationSpeed = 0;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (collectSound != null)
            {
                collectSound.Play();
            }
            TextManager.score += collectableScore;
            Destroy(gameObject);
        }
    }
}