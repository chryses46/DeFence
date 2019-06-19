using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int maxTowers = 5;
    [SerializeField] Tower tower;
    [SerializeField] Transform towerParent;
    [SerializeField] AudioClip dropTower;

    Queue<Tower> towers = new Queue<Tower>();
    
    public void PlaceTower(Waypoint currentWaypoint)
    {
        if (towers.Count < maxTowers)
        {
            InstantiateTower(currentWaypoint);
        }
        else
        {
            MoveTower(currentWaypoint);
        }
    }
    
    private void InstantiateTower(Waypoint currentWaypoint)
    {
        var newTower = Instantiate(tower, currentWaypoint.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(dropTower);
        SetTowerProperties(currentWaypoint, newTower);
    }

    private void MoveTower(Waypoint currentWaypoint)
    {
        if(currentWaypoint.isPlaceable)
        {
            var movingTower = towers.Dequeue();
            movingTower.currentWaypoint.isPlaceable = true;
            movingTower.transform.position = currentWaypoint.transform.position;
            SetTowerProperties(currentWaypoint, movingTower);
        }
    }

    private void SetTowerProperties(Waypoint currentWaypoint, Tower tower)
    {
        tower.currentWaypoint = currentWaypoint;
        tower.transform.parent = towerParent;
        currentWaypoint.isPlaceable = false;
        towers.Enqueue(tower);
    }

}
