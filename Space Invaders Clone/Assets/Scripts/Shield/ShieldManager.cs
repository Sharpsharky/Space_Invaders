using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] private int initialHealth = 3;

    public static event Action<int> OnSetInitialHealth;

    private List<GameObject> shieldHealths = new List<GameObject>();

    private void Awake()
    {
        WaveManager.OnNewGame += InitializeHealth;
        ShieldHealth.OnShieldTouchedByEnemy += TurnEveryShieldOff;
        ShieldHealth.OnShieldSave += AddToShieldList;
    }

    private void AddToShieldList(GameObject shieldHealth)
    {
        shieldHealths.Add(shieldHealth);
    }

    private void InitializeHealth()
    {
        OnSetInitialHealth(initialHealth);
    }

    private void TurnEveryShieldOff()
    {
        foreach(GameObject shieldHealth in shieldHealths)
        {
            if(shieldHealth.activeSelf) shieldHealth.SetActive(false);

        }

    }

}
