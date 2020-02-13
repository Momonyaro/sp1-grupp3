using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Sprite fullHealthIcon;
    public Sprite emptyHealthIcon;

    private void Start()
    {
        CreateHealthIcon(new Vector2(0, 0));
    }

    private Image CreateHealthIcon(Vector2 imagePosition)
    {
        GameObject healthObject = new GameObject("Health", typeof(Image));
        //healthObject.transform.parent = transform;
        healthObject.transform.localPosition = Vector3.zero;

        healthObject.GetComponent<RectTransform>().position = imagePosition;
        healthObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);

        Image healthImage = healthObject.GetComponent<Image>();
        healthImage.sprite = fullHealthIcon;

        return healthImage;
    }
}
