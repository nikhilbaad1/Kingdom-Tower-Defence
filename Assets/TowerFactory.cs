using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    public int towerlimit;
    [SerializeField] Transform towerParentTransform;
    // Start is called before the first frame update
    Queue<Tower> towerQueue = new Queue<Tower>();



    int numTowers = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        numTowers = towerQueue.Count;
        if(numTowers < towerlimit)
        {
            InstantiateNewTower(baseWaypoint);

        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }

    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable=true;
        baseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = baseWaypoint;
        Vector3 temp = baseWaypoint.transform.position;
        temp.y += 5;
        oldTower.transform.position = temp;
        towerQueue.Enqueue(oldTower);
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Vector3 temp = baseWaypoint.transform.position;
        temp.y += 5;
         var newTower = Instantiate(towerPrefab, temp, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;
        numTowers++;
        newTower.baseWaypoint = baseWaypoint;
        towerQueue.Enqueue(newTower);
    }
}
