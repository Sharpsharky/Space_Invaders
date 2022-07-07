using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiveScore : MonoBehaviour
{
    [SerializeField]
    private int scoreForKilling = 10;

    private ScorableEntety enemy;
    private void Awake()
    {
        enemy = GetComponent<ScorableEntety>();
        enemy.OnGetScore += AddScore;
    }

    public void AddScore()
    {
        Results.AddScore(scoreForKilling);
    }
}
