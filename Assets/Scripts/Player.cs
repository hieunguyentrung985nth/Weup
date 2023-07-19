using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;

    private float horizontalInput;

    private float verticalInput;

    private Vector2 moveVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BattleSystem.OnGameOver += BattleSystem_OnGameOver;

        Init();

        gameObject.SetActive(false);
    }

    private void BattleSystem_OnGameOver(object sender, System.EventArgs e)
    {
        enabled = false;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Init()
    {
        rb.gravityScale = 0f;
    }

    private void HandleInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Movement()
    {
        moveVector = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed * Time.fixedDeltaTime;

        rb.velocity = moveVector;
    }
}
