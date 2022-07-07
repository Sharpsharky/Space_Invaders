using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoManager : MonoBehaviour
{
    [SerializeField] private GameObjectPool gameObjectPool;
    [SerializeField] private float minTime = 10;
    [SerializeField] private float maxTime = 20;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    private IEnumerator spawnUfoEnumerator;
    private int newDirection = 1; //direction of movement of the next ufo

    private void Awake()
    {
        WaveManager.OnNewGame += InitializeSpawnUfo;
        WaveManager.OnFinishGame += StopSpawningUfo;
    }

    public void InitializeSpawnUfo()
    {
        float timeToSpawn = DrawTimeToWaitForSpawn();
        spawnUfoEnumerator = SpawnUfo(timeToSpawn);
        StartCoroutine(spawnUfoEnumerator);
    }

    private void StopSpawningUfo()
    {
        if(spawnUfoEnumerator != null)
        {
            StopCoroutine(spawnUfoEnumerator);
        }
    }


    private IEnumerator SpawnUfo(float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);
        
        Transform spawn = DrawPosition();
        GameObject newUfo = gameObjectPool.Get();
        
        newUfo.transform.position = spawn.position;
        newUfo.GetComponent<UfoMovement>().SetDirection(newDirection);
        InitializeSpawnUfo();
        yield return null;
    }

    private float DrawTimeToWaitForSpawn()
    {
        return Random.Range(minTime, maxTime);
    }

    private Transform DrawPosition()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            newDirection = -1;
            return leftSpawn;
        }
        else
        {
            newDirection = 1;
            return rightSpawn;
        }
    }

}
