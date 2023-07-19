using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnScoreChanged;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private float initialScore;

    private float currentScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;

        Enemy.OnEnemyDestroyed += Enemy_OnEnemyDestroyed;

        BattleSystem.OnGameOver += BattleSystem_OnGameOver;

        currentScore = initialScore;

        SetText(initialScore);

        Hide();
    }

    private void BattleSystem_OnGameOver(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        Show();
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDestroyed -= Enemy_OnEnemyDestroyed;
    }

    private void Enemy_OnEnemyDestroyed(object sender, Enemy.OnEnemyDestroyedEventArgs e)
    {
        SetText(e.score);
    }

    private void SetText(float value)
    {
        currentScore += value;

        scoreText.text = "Score: " + currentScore.ToString();

        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetScore()
    {
        return currentScore;
    }

    private void Hide()
    {
        scoreText.gameObject.SetActive(false);
    }

    private void Show()
    {
        scoreText.gameObject.SetActive(true);
    }
}
