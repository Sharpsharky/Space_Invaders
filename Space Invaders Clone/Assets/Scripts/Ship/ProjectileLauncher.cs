using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;

    [SerializeField] protected GameObjectPool projectilePool;
    
    protected float fireRate = 1;


    private void Awake()
    {
        GetComponent<IEntetyImputable>().OnFire += ShootProjectile;

    }

    protected void ShootProjectile()
    {        
        GameObject newProjectile = projectilePool.Get();
        newProjectile.gameObject.transform.position = shootingPoint.position;
        newProjectile.gameObject.transform.eulerAngles = shootingPoint.eulerAngles + new Vector3(0, 180, 0);
        if (!newProjectile.activeSelf) newProjectile.SetActive(true);
        
    }



}
