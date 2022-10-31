using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public event Action OnTakeDamage;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        OnTakeDamage?.Invoke();

        Debug.Log(currentHealth);
    }
}
