using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force;

    [SerializeField] private float damage;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        rb.gravityScale = 0f;
    }

    public void Move(Vector2 moveVector)
    {
        rb.AddForce(moveVector * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
