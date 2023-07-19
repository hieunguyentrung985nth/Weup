using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePointTransform;

    [SerializeField] private GameObject bulletPref;

    [SerializeField] private float timeDelayMax;

    [SerializeField] private float timeDestroyBullet;

    private float timeDelay;

    private void Start()
    {
        BattleSystem.OnGameOver += BattleSystem_OnGameOver;
    }

    private void BattleSystem_OnGameOver(object sender, System.EventArgs e)
    {
        enabled = false;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (timeDelay == 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                SpawnBullet();

                timeDelay += Time.deltaTime;
            }
        }       

        else
        {
            timeDelay += Time.deltaTime;

            if (timeDelay >= timeDelayMax)
            {
                timeDelay = 0f;
            }
        }
    }

    private void SpawnBullet()
    {
        GameObject bulletSpawn = Instantiate(bulletPref, firePointTransform.position, Quaternion.identity);

        Vector2 shootVector = firePointTransform.transform.up;

        Bullet bullet = bulletSpawn.GetComponent<Bullet>();

        bullet.Move(shootVector);

        Destroy(bullet.gameObject, timeDestroyBullet);
    }
}
