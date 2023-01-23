using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    // Enemies
    [Header("Enemies")]
    [Space]
    public List<GameObject> enemiesPrefabs;
    public int numberOfEnemies = 5;

    // Spawner limits
    [Space]
    [Header("Spawner Limits")]
    [Space]
    public float minX = -12f;
    public float maxX = 12f;
    public float minY = -8.5f;
    public float maxY = 8.5f;

    // Levels
    [Space]
    [Header("Levels")]
    [Space]
    public int numberOfLevels = 5;
    public TextMeshProUGUI levelText;


    // Current level
    private int currentLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        // Check if all enemies have been killed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            // Increase number of enemies to be spawned and the level
            numberOfEnemies += currentLevel;
            currentLevel++;
            
            // If passed the last level, go to MainScene
            if(currentLevel > numberOfLevels)
            {
                SceneManager.LoadScene("MainScene");
            }

            // If not, spawn enemies
            else
            {
                SpawnEnemies();
            }
        }
    }

    /// <summary>
    /// Spawn enemies.
    /// </summary>
    private void SpawnEnemies()
    {
        // Display level info
        levelText.text = "Level " + currentLevel.ToString() + " / " + numberOfLevels.ToString();

        // Spawn enemies
        for(int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)], position, Quaternion.identity);
        }
    }
}
