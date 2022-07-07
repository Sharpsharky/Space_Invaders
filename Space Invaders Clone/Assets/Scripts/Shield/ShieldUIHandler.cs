using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;

    private ShieldHealth shieldHealth;

    private void Awake()
    {
        shieldHealth = GetComponent<ShieldHealth>();
        shieldHealth.OnSetUIText += ChangeUIText;
    }

    private void ChangeUIText(int health)
    {
        healthText.text = health.ToString();

    }

}
