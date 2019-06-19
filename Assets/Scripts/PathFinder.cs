using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    [SerializeField] Waypoint startPoint, endPoint;

    bool isRunning = true;

    Waypoint searchCenter;

    List<Waypoint> path = new List<Waypoint>(); // Empty list to hold path waypoints

    bool pathCalculated = false;

    public List<Waypoint> GetPath()
    {
        if (!pathCalculated)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        
        LoadBlocks();
        BreadthFirstSearch();
        FormPath();
        pathCalculated = true;
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>(); // creates array of waypoints of type Waypoints

        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();

            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping cube " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    private void BreadthFirstSearch()
    {

        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning)
        {

            searchCenter = queue.Dequeue();
            CheckForEnd();
            ExploreNeighbors();
            searchCenter.isSearched = true;
        }
    }

    private void CheckForEnd()
    {
        if (searchCenter == endPoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighborCoordinates))
            {
                QueueNewNeighbor(neighborCoordinates);
            }
        }
    }

    private void FormPath()
    {
        SetAsPath(endPoint);
        

        Waypoint previous = endPoint.exploredFrom;

        while (previous != startPoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startPoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void QueueNewNeighbor(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];

        if (neighbor.isSearched || queue.Contains(neighbor))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }

    
}
