using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject enemyGameObject;
    [SerializeField] Transform enemySpawnPoint;
    GameObject newEnemyInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            newEnemyInstance = Instantiate(enemyGameObject, enemySpawnPoint.position, Quaternion.identity);
        }
    }
}
