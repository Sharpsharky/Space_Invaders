using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private RegenerateWave regenerateWave;
    private EnemyShooting enemyShooting;

    public event Action OnStartWave = delegate { };
    
    public static event Action OnNewGame = delegate { };
    public static event Action OnFinishGame;
    public static event Action<bool> OnTurnOnPlayer;
    public static event Action OnPlayerSetHP;
    public static event Action OnPlayerChangePos;

    private void Awake()
    {
        regenerateWave = GetComponent<RegenerateWave>();
        enemyShooting = GetComponent<EnemyShooting>();

        ButtonsHandler.OnGoToMenu += RemoveWave;
        ButtonsHandler.OnStartGame += StartGame;

        enemyShooting.OnNextWave += NextWave;
    }
    private void Start()
    {
        PlayerHealth.OnDie += EndGame;
        CheckEnemy.OnEndGame += EndGame;
        OnTurnOnPlayer(false);
            
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        OnNewGame();
        Results.ResetResults();
        NextWave();
        OnTurnOnPlayer(true);
        OnPlayerSetHP();
    }

    public void NextWave()
    {
        OnStartWave();
        Results.AddWave();
        regenerateWave.StartInitializedWave();

    }
    
    //Hide all enemies, projectiles, player
    public void EndGame()
    {
        OnFinishGame();
        RemoveWave();
    }

    private void RemoveWave()
    {
        regenerateWave.InitializeWave();
        OnTurnOnPlayer(false);
        OnPlayerChangePos();

    }

}
