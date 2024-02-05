using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public event Action OnTakeDamage;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) return;

        //takes the highest number between health after damage and 0, ensures health never falls below 0
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke();

        Debug.Log(currentHealth);
    }

}
