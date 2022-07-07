using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooting : MonoBehaviour
{
   // [SerializeField]
    //private HashSet<GameObject> enemies = new HashSet<GameObject>();
    [SerializeField]
    private HashSet<EnemyInput> enemyInputs = new HashSet<EnemyInput>();
    [SerializeField]
    private float initialTimeBetweenShots = 2; //const
    [SerializeField]
    private float increaseShootingCoef = 0.95f; //const

    private float currentTimeBetweenShots = 2;
    private bool isShooting = false;
    private IEnumerator shooting;

    public event Action OnNextWave = delegate { };


    public HashSet<EnemyInput> EnemyInputs { get => enemyInputs; set => enemyInputs = value; }

    private void Awake()
    {

        EnemyWaveMovement.OnIncrementSpeed += IncreaseEnemyShooingSpeed;
        EnemyWaveMovement.OnResetSpeed += ResetShootingSpeed;
        EnemyInput.OnAddEnemyInputToHashSet += AddEnemyInputToHashSet;
        Enemy.OnRemoveEnemyFromHashSet += RemoveEnemyFromHashSet;
    }


    private void AddEnemyInputToHashSet(EnemyInput enemyInput)
    {
        EnemyInputs.Add(enemyInput);
    }

    private void Start()
    {
        RegenerateWave.instance.StopShooting += StopShooiting;
    }

    private void ResetShootingSpeed()
    {
        currentTimeBetweenShots = initialTimeBetweenShots;
    }

    private void IncreaseEnemyShooingSpeed()
    {
        currentTimeBetweenShots *= increaseShootingCoef;
    }

    public void StartShooting()
    {
        isShooting = true;
        shooting = ShootWithDelay();
        StartCoroutine(shooting);
    }

    public void RemoveEnemyFromHashSet(EnemyInput enemyInput)
    {
        enemyInputs.Remove(enemyInput);
        CheckEnemiesAlive();
    }

    private void CheckEnemiesAlive()
    {
        if (enemyInputs.Count > 0) return;

        OnNextWave();
    }

    private IEnumerator ShootWithDelay()
    {
        while (isShooting)
        {
            yield return new WaitForSeconds(currentTimeBetweenShots);
            DrawNewEnemy()?.Shoot();
        }
            yield return null;
    }



    private EnemyInput DrawNewEnemy()
    {

        int i = 0;
        int random = Random.Range(0, enemyInputs.Count);
        foreach (EnemyInput enemy in enemyInputs)
        {
            if (i == random) return enemy;
            i++;
        }

        return null;
    }

    private void StopShooiting()
    {
        isShooting = false;
        enemyInputs = new HashSet<EnemyInput>();
        if(shooting != null) StopCoroutine(shooting);
    }



}
