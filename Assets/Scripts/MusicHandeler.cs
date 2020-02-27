using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandeler : MonoBehaviour
{
    [SerializeField] AudioSource mainLoop = null;

    void Start()
    {
        
    }

    void Update()
    {
        mainLoop.Play();
    }
}
