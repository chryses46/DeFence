using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,10f)]
    [SerializeField] float timeBetweenSpawns = 3f;
    [SerializeField] EnemyMover enemyObject;
    [SerializeField] Transform enemyParent;
    [SerializeField] AudioClip enemySpawn;
    [SerializeField] Text playerScoreText;
    int playerScore = 0;

    //PlayerHealth playerHealth;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        playerScoreText.text = playerScore.ToString();
        //playerHealth = GetComponent<PlayerHealth>();
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var enemy = Instantiate(enemyObject, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(enemySpawn);
            enemy.transform.parent = enemyParent;
            playerScore++;
            playerScoreText.text = playerScore.ToString();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
   
}
