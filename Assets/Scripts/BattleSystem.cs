using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static event EventHandler OnGameOver;

    [SerializeField] private List<Wave> wavesList;

    private int totalEnemies;

    private void Start()
    {
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;

        Enemy.OnEnemyDestroyed += Enemy_OnEnemyDestroyed;
    }

    private void Enemy_OnEnemyDestroyed(object sender, Enemy.OnEnemyDestroyedEventArgs e)
    {
        HandleEnemiesCount();
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        foreach (Wave wave in wavesList)
        {
            totalEnemies += wave.GetEnemyBlocks().Count;
        }
    }

    private void HandleEnemiesCount()
    {
        totalEnemies = totalEnemies - 1;

        if (totalEnemies == 0)
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
