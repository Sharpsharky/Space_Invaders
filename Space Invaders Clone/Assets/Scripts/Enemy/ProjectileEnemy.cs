using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Projectile
{
    [SerializeField] private float movementSpeed = 10;

    protected override void MoveLogic()
    {

        transform.Translate(-Vector3.forward * projectileConfig.MoveSpeed * Time.deltaTime * movementSpeed);
    }

    

}
