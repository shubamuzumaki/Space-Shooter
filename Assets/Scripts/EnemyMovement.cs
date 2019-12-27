using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private WaveConfig waveConfig;
    private float moveSpeed;

    private bool isWaveConfigSet = false;
    private List<Transform> wayPoints;
    private int wayPointInd = 0;
    private int wayPointSize = 0;

    void SetUpWayPoints()
    {
        wayPoints = waveConfig.GetWayPoints();
        wayPointSize = wayPoints.Count;
        transform.position = wayPoints[0].position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        moveSpeed = waveConfig.GetEnemyMoveSpeed();
        SetUpWayPoints();

        isWaveConfigSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    private void Move()
    {
        if(!isWaveConfigSet)
            return;
            
        if(wayPointInd < wayPointSize)
        {
            var targetPos = wayPoints[wayPointInd].position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if(transform.position == targetPos)
                wayPointInd++;
        }
        else
            Destroy(this.gameObject);
    }
}
