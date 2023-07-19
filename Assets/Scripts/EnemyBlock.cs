using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [SerializeField] private List<Waypoint> wavepointsList;

    [SerializeField] private float timeDelayMax;

    private float timeDelay;

    private bool startDelay;

    private Wave wave;

    private Waypoint currentWaypoint;

    private MoveType currentMove;

    private int currentIndexWaypoint;

    private int currentIndexWaypointList;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Delay();
    }

    private void Init()
    {
        enemy.SpawnAtPosition(transform.position);

        enemy.gameObject.SetActive(false);

        currentWaypoint = wavepointsList[currentIndexWaypoint];

        currentMove = currentWaypoint.moveType;
    }

    public void NextWayPointIndex()
    {
        currentIndexWaypointList++;

        if (currentIndexWaypointList == currentWaypoint.wayPointsList.Count)
        {
            currentIndexWaypoint++;

            if (currentIndexWaypoint == wavepointsList.Count)
            {
                currentWaypoint = null;

                currentIndexWaypoint = default;

                currentIndexWaypointList = default;

                enemy.TurnOnCollider();

                return;
            }

            currentIndexWaypointList = 0;

            currentWaypoint = wavepointsList[currentIndexWaypoint];

            currentMove = currentWaypoint.moveType;
        }

        if (currentWaypoint != null)
        {
            startDelay = true;
        }
    }

    public void NextWayPointList()
    {
        currentIndexWaypointList++;

        enemy.SetUpPoint(currentWaypoint.wayPointsList[currentIndexWaypointList]);
    }

    private void Delay()
    {
        if (startDelay)
        {
            timeDelay += Time.deltaTime;

            if (timeDelay >= timeDelayMax)
            {
                timeDelay = 0f;

                startDelay = false;

                enemy.SetUpPoint(currentWaypoint.wayPointsList[currentIndexWaypointList]);
            }
        }
    }

    public void EnemyMoving(Wave wave)
    {
        enemy.gameObject.SetActive(true);

        this.wave = wave;

        enemy.SetWaveAndEnemyBlock(wave, this);

        enemy.SetUpPoint(currentWaypoint.wayPointsList[currentIndexWaypointList]);
    }

    public bool CompleteCurrentWayPointList()
    {
        if (currentIndexWaypointList == currentWaypoint.wayPointsList.Count - 1)
        {
            return true;
        }

        return false;
    }
}
