using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentTransform = transform.position;
        transform.position = new Vector3(currentTransform.x, player.transform.position.y, currentTransform.z);
    }
}
