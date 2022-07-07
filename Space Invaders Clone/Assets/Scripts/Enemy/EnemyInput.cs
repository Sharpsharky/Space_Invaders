using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IEntetyImputable
{
    public event Action OnFire = delegate { };
    public static event Action<EnemyInput> OnAddEnemyInputToHashSet = delegate { };

    private void OnEnable()
    {
        OnAddEnemyInputToHashSet(this);
    }

    public void Shoot()
    {
        OnFire();
    }


}
