using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] string loadScene;

    void Start()
    {
        
    }

    void Update()
    {
        
    }



    public void LoadMap(string mapName)
    {
        var thisScene = mapName;
        if (mapName.Equals("this")) thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(loadScene);
        }
    }
}
