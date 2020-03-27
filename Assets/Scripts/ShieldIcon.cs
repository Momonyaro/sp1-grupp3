using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldIcon : MonoBehaviour
{
    //BoatMovementV01 boat;
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    void Update()
    {
        if(TextManager.shieldCoinsAmount >= 10)
        {
            GetComponent<Image>().color = activeColor;
        }
        else
        {
            GetComponent<Image>().color = inactiveColor;
        }
    }
}
