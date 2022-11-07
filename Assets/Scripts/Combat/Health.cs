using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    private bool isInvulnerable;

    public bool IsDead => currentHealth == 0; // conditional property

    public event Action OnTakeDamage;
    public event Action OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return;
        }          

        if (isInvulnerable)
            return;

        OnTakeDamage?.Invoke();
        currentHealth = Mathf.Max(currentHealth - damage, 0);        

        Debug.Log(currentHealth);

        if (currentHealth == 0)
            OnDie?.Invoke();
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public float GetHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }
}
