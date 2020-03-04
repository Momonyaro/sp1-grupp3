using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHandeler : MonoBehaviour
{
    int counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDown()
    {
        counter += 1;
        Debug.Log(counter);
    }

    public void pickedUp()
    {
        counter -= 1;
        Debug.Log(counter);
    }
}
