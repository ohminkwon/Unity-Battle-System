using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Health health;

    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        health.OnTakeDamage += Health_OnTakeDamage;

        UpdateHealthBar();

        health.OnDie += health_Ondie;        
    }
    private void LateUpdate()
    {
        transform.LookAt(cameraTransform);
    }

    private void Health_OnTakeDamage()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health.GetHealthNormalized();
    }
    
    private void health_Ondie()
    {
        healthBar.fillAmount = 0;
        // TODO : Hide UI
    }
}
