using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour, IDemageble
{
    public event Action<int> OnSetUIText = delegate { };

    private int currentHealth = 3;
    public event Action OnTurnEveryShieldOff = delegate { };

    public static event Action OnShieldTouchedByEnemy;
    public static event Action<GameObject> OnShieldSave;

    private void Awake()
    {
        ShieldManager.OnSetInitialHealth += SetHealth;
    }

    private void Start()
    {
        OnShieldSave(gameObject);
        gameObject.SetActive(false);
    }

    private void SetHealth(int healthToSet)
    {
        currentHealth = healthToSet;
        OnSetUIText(currentHealth);

        if (currentHealth > 0 && !gameObject.activeSelf) gameObject.SetActive(true);
    }

    public void DealDamage(int damageAmount)
    {
        currentHealth--;
        OnSetUIText(currentHealth);
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy") return;

        OnShieldTouchedByEnemy();
    }

}
