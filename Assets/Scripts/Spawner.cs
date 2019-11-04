using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int level, maxEnemy = 5;
    public float delay = 2.0f;
    public GameObject enemy;

    private int enemySpawned;
    private bool canSpawn = true;

    private void Update() {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (level == 1){
            if(canSpawn && enemySpawned < maxEnemy)
            {
                StartCoroutine(SpawnCo(delay));
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
        } else {
            Debug.Log("The spawner "+ gameObject.name+" has a level out of range (Should be between 1 and 5)");
        }
        
    }

    IEnumerator SpawnCo(float delay)
    {
        canSpawn = false;
        enemySpawned++;
        yield return new WaitForSeconds(this.delay);
        canSpawn = true;
    }

}
