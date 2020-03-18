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
    BoatMovementV01 boatMv;
    private readonly UIShake _uiShake = new UIShake();
    private int _oldHealth;

    void Start()
    {
        boatMv = FindObjectOfType<BoatMovementV01>();
        healthAmount = boatMv.maxHealth;
        CreateHealth();
    }

    private void Update()
    {
        healthAmount = BoatMovementV01.currentHealth;
        if (_oldHealth != healthAmount)
        {
            UpdateContainers();
        }
        _uiShake.Update();
    }

    public void CreateHealth()
    {
        for(int i = 0; i < healthAmount; i++)
        {
            healthIcons[i].gameObject.SetActive(true);
            healthIcons[i].sprite = fullContainer;
        }

        _oldHealth = healthAmount;
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

        Vector3 oldPos = transform.position;
        _uiShake.BlinkAndMove(gameObject, new Vector3(oldPos.x + 20, oldPos.y, oldPos.x));

        _oldHealth = healthAmount;
    }
}
