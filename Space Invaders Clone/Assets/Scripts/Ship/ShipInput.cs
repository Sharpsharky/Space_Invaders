using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipInput : MonoBehaviour, IEntetyImputable
{

    private float nextFire = 0;
    protected float fireRate = 1;

    public float Thrust { get; private set; }

    public event Action OnFire = delegate { };

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        fireRate = GetComponent<PlayerConfigHolder>().ShipSettings.ShootingSpeed;
    }

    void Update()
    {
        Thrust = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.Space)) ShootLogic();

    }

    private void ShootLogic()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            OnFire();
        }
    }
}
