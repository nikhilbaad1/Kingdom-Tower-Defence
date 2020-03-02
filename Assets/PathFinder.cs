using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    
    public List<Waypoint> Path = new List<Waypoint>();
    public Waypoint startWayPoint, endWayPoint;
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRuning = true;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = { 
    Vector2Int.up,
    Vector2Int.left,
    Vector2Int.right,
    Vector2Int.down
    };

    Waypoint searchCenter;
    // Start is called before the first frame update


    private void CreatePath()
    {
        SetAsPath(endWayPoint);
        Waypoint previous = endWayPoint.exploredFrom;
        while (previous != startWayPoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;      
        }
        //reversing the list
        SetAsPath(startWayPoint);
        Path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        Path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    public List<Waypoint> GetPath()
    {
        if (Path.Count <= 0)
        {
            LoadBlocks();
            //ColorStartAndEnd();
            BreadthFirstSearch();
            CreatePath();
        }

        return Path;
    }

    private void BreadthFirstSearch()
    {
       queue.Enqueue(startWayPoint);
        while (queue.Count > 0 && isRuning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndPoint();
            ExploreNeighbours();
            searchCenter.isEplored = true;
        }
        print("Finished path finding");

    }

    private void HaltIfEndPoint()
    {
        if(searchCenter == endWayPoint)
        {
            print("Found");
            isRuning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRuning) return;

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.getGridPos() + direction; 
            print("Exploring" + neighbourCoordinates);
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
                
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isEplored || queue.Contains(neighbour))
        {

        }
        else
        {
            if (neighbour == endWayPoint)
            {
                print("Endpoint found");
                neighbour.exploredFrom = searchCenter;
                isRuning = false;
                
            }
            else
            {
                queue.Enqueue(neighbour);
                neighbour.exploredFrom = searchCenter;
                print("Queueing " + neighbour);
            }
        }
    }

   /* private void ColorStartAndEnd()
    {
        startWayPoint.SetTopColor(Color.green);
        endWayPoint.SetTopColor(Color.red);
    }*/

    private void LoadBlocks()
    {
    
        var waypoints = FindObjectsOfType<Waypoint>();
        //print(waypoints.);
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.getGridPos());
            if (isOverlapping)
            {
                Debug.LogError("Overlapping waypoint " + waypoint);
            }
            else
            {
                grid.Add(waypoint.getGridPos(), waypoint);
               // waypoint.SetTopColor(Color.white);
            }
            
        }
        print(grid.Count);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
