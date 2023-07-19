using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private List<EnemyBlock> enemyBlocksList;

    private List<EnemyBlock> enemyBlockDoneMovingList;

    private void Awake()
    {
        enemyBlockDoneMovingList = new List<EnemyBlock>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        StartEnemyMoving();
    }

    private void StartEnemyMoving()
    {
        foreach (EnemyBlock block in enemyBlocksList)
        {
            block.EnemyMoving(this);
        }
    }

    private void Update()
    {
        CheckIfDoneMoving();
    }

    private void CheckIfDoneMoving()
    {
        if (enemyBlockDoneMovingList.Count == enemyBlocksList.Count)
        {
            MoveToNextWayPoint();
        }
    }

    public void AddToDictionary(EnemyBlock enemyBlock)
    {
        if (enemyBlock.CompleteCurrentWayPointList())
        {
            enemyBlockDoneMovingList.Add(enemyBlock);
        }

        else
        {
            enemyBlock.NextWayPointList();
        }
    }

    public bool MoveToNextWayPointList()
    {
        foreach (EnemyBlock block in enemyBlocksList)
        {
            if (!block.CompleteCurrentWayPointList())
            {
                block.NextWayPointList();

                return false;
            }
        }

        return true;
    }

    public void MoveToNextWayPoint()
    {
        foreach (EnemyBlock block in enemyBlocksList)
        {
            block.NextWayPointIndex();
        }

        enemyBlockDoneMovingList.Clear();
    }

    public List<EnemyBlock> GetEnemyBlocks()
    {
        return enemyBlocksList;
    }
}
