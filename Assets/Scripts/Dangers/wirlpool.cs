using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wirlpool : MonoBehaviour
{
    BoatMovementV01 boat;
    bool whirl = false;
    public float intoWhirlSpeed = 8f;
    bool activated = false;

    void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }

    void Update()
    {
        if (activated)
        {
            MoveBoat();
        }
    }

    public void MoveBoat()
    {
        boat.transform.position = Vector3.MoveTowards(boat.transform.position, transform.position, intoWhirlSpeed * Time.deltaTime);
        if (transform.position == boat.transform.position)
        {
            boat.StopBoatSwitchBool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = true;
    }
}
