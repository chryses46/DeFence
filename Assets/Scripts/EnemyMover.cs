using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float dwellTime = .5f;
    [SerializeField] GameObject endFX;
    GameObject player;
    //PlayerHealth playerHealth;

    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        //playerHealth = GetComponent<PlayerHealth>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
            foreach (Waypoint waypoint in path)
            {
                transform.position = waypoint.transform.position;
                yield return new WaitForSeconds(dwellTime);
            }

            ReachEnd();
    }

    private void ReachEnd()
    {
        var fx = Instantiate(endFX, transform.position, Quaternion.identity);
        var fxTime = fx.GetComponent<ParticleSystem>().main.duration;
        Destroy(fx, fxTime);
        Destroy(gameObject);
    }
}
