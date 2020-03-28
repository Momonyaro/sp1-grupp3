using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    public VideoPlayer videoplayer;
    public string nextScene;
    float countdown = 2f;
    void Start()
    {
        videoplayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            if (!videoplayer.isPlaying || Input.anyKeyDown)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
