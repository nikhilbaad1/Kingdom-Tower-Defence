using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0f,12f)]
    [SerializeField] float secondsBetweenSpawn = 0.5f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnEnemies;
    int score=0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemies.text = score.ToString();
    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            score++;
            var newEnemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
     
    }
}
