using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Start()
    {
        BattleSystem.OnGameOver += BattleSystem_OnGameOver;

        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;

        Hide();
    }

    private void ScoreManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        SetScoreText();
    }

    private void BattleSystem_OnGameOver(object sender, System.EventArgs e)
    {
        Show();
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
