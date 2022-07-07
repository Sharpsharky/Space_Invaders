using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] protected ProjectileConfig projectileConfig;

    [SerializeField] private List<string> objectTagsToKill = new List<string>();

    private float currentLifeTime = 0;
    private GameObjectPool pool;
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

    protected virtual void OnEnable()
    {
        currentLifeTime = 0;
    }

    public void DestroyGameObj()
    {
        pool.ReturnToPool(gameObject);
    }

    private void Update()
    {
        MoveLogic();
        CheckLifeTime();
    }

    protected virtual void MoveLogic()
    {
        transform.Translate(-Vector3.forward * projectileConfig.MoveSpeed * Time.deltaTime);
    }

    private void CheckLifeTime()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= projectileConfig.LivingTime) DestroyGameObj();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (CheckIfObjectTagInList(other.tag))
        {
            other.gameObject.GetComponent<IDemageble>().DealDamage(1);
            DestroyGameObj();
        }
    }

    private bool CheckIfObjectTagInList(string objTag)
    {
        if (objectTagsToKill.Count == 0) return false;

        foreach(string reservedTag in objectTagsToKill)
        {
            if (reservedTag == objTag) return true;
        }

        return false;
    }

}
