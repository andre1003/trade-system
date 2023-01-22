using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;

    [Space]
    public float minX = -12f;
    public float maxX = 12f;
    public float minY = -8.5f;
    public float maxY = 8.5f;

    [Space]
    public int numberOfLevels = 5;


    private int currentLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            numberOfEnemies += currentLevel;
            currentLevel++;
            
            if(currentLevel >= numberOfLevels)
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SpawnEnemies();
            }
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }
}
