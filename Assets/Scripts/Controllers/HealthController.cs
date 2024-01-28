using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthController
{
    private float currentHealth;
    public float maxHealth;
    public event Action OnZeroHealth;
    public HealthController(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float healthChange)
    {
        currentHealth -= healthChange;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            OnZeroHealth.Invoke();
        }
    }
}
