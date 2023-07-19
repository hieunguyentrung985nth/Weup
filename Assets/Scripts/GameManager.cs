using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStart;

    [SerializeField] private Transform playerSpawnPos;

    [SerializeField] private Player player;

    [SerializeField] private Button startButton;

    private void Awake()
    {
        Instance = this;

        startButton.onClick.AddListener(SpawnPlayer);
    }

    private void Start()
    {
        
    }

    private void SpawnPlayer()
    {
        player.transform.position = playerSpawnPos.position;

        player.gameObject.SetActive(true);

        startButton.gameObject.SetActive(false);

        OnGameStart?.Invoke(this, EventArgs.Empty);
    }
}
