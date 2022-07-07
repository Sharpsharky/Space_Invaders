using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoHealth : ScorableEntety, IDemageble, IGameObjectPooled
{
    [SerializeField] private int initialHp = 1;
    [SerializeField] private float lifeTime = 10;

    private int currentHealth = 1;

    public event Action OnDestroy = delegate { };

    private GameObjectPool pool;
    private IEnumerator lifeTimeEnumerator;

    private void Awake()
    {
        WaveManager.OnFinishGame += DestroyUfo;
    }

    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            DestroyUfo();
            GetScore();
        }
    }

    private void OnEnable()
    {
        currentHealth = initialHp;
        lifeTimeEnumerator = DestroyUfoAfterTime();
        StartCoroutine(lifeTimeEnumerator);
    }
    private void DestroyUfo()
    {
        if (lifeTimeEnumerator != null) StopCoroutine(lifeTimeEnumerator);
        ReturnToPool();
    }

    private IEnumerator DestroyUfoAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        DestroyUfo();

        yield return null;

    }

    private void ReturnToPool()
    {
        pool.ReturnToPool(gameObject);
    }

    public GameObjectPool Pool
    {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("Bad pool");
        }
    }

}
