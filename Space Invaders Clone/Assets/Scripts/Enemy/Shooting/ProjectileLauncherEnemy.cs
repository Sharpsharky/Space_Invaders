using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherEnemy : ProjectileLauncher
{

    public void SetPool(GameObjectPool gameObjectPool)
    {
        projectilePool = gameObjectPool;
    }
}
