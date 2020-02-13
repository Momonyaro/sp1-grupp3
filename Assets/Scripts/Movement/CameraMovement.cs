using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float deadzone = 5;
    [SerializeField] GameObject player;
    public float looks = 100f;
    void Start()
    {
        if (FindObjectOfType<BoatMovementV01>())
            player = FindObjectOfType<BoatMovementV01>().gameObject;
        else
            player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= deadzone)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + Vector3.up * 3, Vector2.Distance(transform.position, player.transform.position) / looks);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
