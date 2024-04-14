using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Recebe os objetos dos inimigos
    [SerializeField] private GameObject enemy01Object;
    [SerializeField] private GameObject enemy02Object;

    private float levelStartTimer;

    [SerializeField] private int currentLevel;

    private int pointsToLevelUp;
    [SerializeField] private int playerTotalPoints;

    private bool isLevelStarted;

    private int spawnerMaxSimultaneousSpawns;
    private int spawnerMinSimultaneousSpawns;

    //Variável que controla o timer do spawner de inimigos
    private float spawnerTimer;
    private float spawnerTimerMinRange;
    private float spawnerTimerMaxRange;

    private float enemy02SpawnRate;

    private bool canSpawnBoss;

    // Start is called before the first frame update
    void Start()
    {
        levelStartTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (isLevelStarted)
        {
            if(canSpawnBoss)
            {
                //SpawnBoss();
            } else
            {
                SpawnCommomEnemies();
            }
        }
    }

    private void SpawnCommomEnemies()
    {
        spawnerTimer -= Time.deltaTime;
        if (spawnerTimer <= 0)
        {
            spawnerTimer = Random.Range(spawnerTimerMinRange, spawnerTimerMaxRange);

            int simultaneousSpawns = Random.Range(spawnerMinSimultaneousSpawns, spawnerMaxSimultaneousSpawns + 1);

            for (int i = 0; i < simultaneousSpawns; i++)
            {
                float spawnChance = Random.Range(0f, 1f);

                if (spawnChance > enemy02SpawnRate)
                {
                    Instantiate(enemy01Object, new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 13f), 0), transform.rotation);
                }
                else
                {
                    Instantiate(enemy02Object, new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 13f), 0), transform.rotation);
                }

            }
        }
    }

    private void SpawnBoss()
    {
        Debug.Log("O Boss chegou");
    }

    private void ChangeLevelDifficult()
    {
        switch (currentLevel)
        {
            case 1:
                pointsToLevelUp = 100;
                spawnerTimerMaxRange = 5;
                spawnerTimerMinRange = 3;
                spawnerMaxSimultaneousSpawns = 1;
                spawnerMinSimultaneousSpawns = 1;
                break;
            case 2:
                pointsToLevelUp = 250;
                spawnerTimerMaxRange = 4;
                spawnerTimerMinRange = 2;
                spawnerMaxSimultaneousSpawns = 2;
                spawnerMinSimultaneousSpawns = 1;
                enemy02SpawnRate = 0.15f;
                break;
            case 3:
                pointsToLevelUp = 500;
                spawnerTimerMaxRange = 3;
                spawnerTimerMinRange = 0.5f;
                spawnerMaxSimultaneousSpawns = 4;
                spawnerMinSimultaneousSpawns = 2;
                enemy02SpawnRate = 1f;
                break;
            case 4:
                canSpawnBoss = true;
                break;
        }
    }

    private void LevelUp()
    {
        if (playerTotalPoints >= pointsToLevelUp && currentLevel < 4)
        {
            currentLevel++;
            levelStartTimer = 5;
            isLevelStarted = false;

            ChangeLevelDifficult();
        }

        if(levelStartTimer <= 0)
        {
            isLevelStarted = true;
        } else
        {
            levelStartTimer -= Time.deltaTime;
        }
    }

    public void AddPoints(int points)
    {
        playerTotalPoints += points;
    }

    public bool getBossSpawnCondition()
    {
        return canSpawnBoss;
    }
}
