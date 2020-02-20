using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] healthIcons;
    public Sprite fullContainer;
    public Sprite emptyContainer;
    [HideInInspector] public int healthAmount;
    //[HideInInspector] public int playerCurrentHealth;

    void Start()
    {
        healthAmount = BoatMovementV01.maxHealth;
        CreateHealth();
    }

    private void Update()
    {
        healthAmount = BoatMovementV01.currentHealth;
        UpdateContainers();
    }

    public void CreateHealth()
    {
        for(int i = 0; i < healthAmount; i++)
        {
            healthIcons[i].gameObject.SetActive(true);
            healthIcons[i].sprite = fullContainer;
        }
    }

    public void UpdateContainers()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].sprite = emptyContainer;
        }
        for(int i = 0; i < healthAmount; i++)
        {
            healthIcons[i].sprite = fullContainer;
        }
    }
}
