using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;

    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public float score;
    }

    [SerializeField] private float moveSpeed;

    [SerializeField] private float minDistance;

    [SerializeField] private Collider2D enemyCollider;

    [SerializeField] private float heal;

    [SerializeField] private float score;

    private Rigidbody2D rb;

    private Wave wave;

    private EnemyBlock enemyBlock;

    private Vector2 currentMovePoint;

    private Vector2 moveDir;

    private bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (canMove && Vector2.Distance(transform.position, currentMovePoint) <= minDistance)
        {
            canMove = false;

            transform.position = currentMovePoint;

            wave.AddToDictionary(enemyBlock);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Init()
    {
        rb.gravityScale = 0f;

        TurnOffCollider();
    }

    public void SetWaveAndEnemyBlock(Wave wave, EnemyBlock enemyBlock)
    {
        this.wave = wave;

        this.enemyBlock = enemyBlock;
    }

    public void SetUpPoint(Transform point)
    {
        this.currentMovePoint = point.position;

        moveDir = (currentMovePoint - (Vector2)transform.position).normalized;

        canMove = true;
    }

    public void SpawnAtPosition(Vector2 spawnPos)
    {
        transform.position = spawnPos;
    }

    public bool CanMove()
    {
        return canMove;
    }

    private void HandleMovement()
    {
        if (canMove)
        {
            //rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

            transform.position = Vector2.MoveTowards(transform.position, currentMovePoint, moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void TurnOnCollider()
    {
        enemyCollider.enabled = true;
    }

    private void TurnOffCollider()
    {
        enemyCollider.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        heal = Mathf.Clamp(heal - damage, 0f, heal);

        if (heal == 0)
        {
            OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs
            {
                score = score
            });

            Destroy(gameObject);
        }
    }
}
