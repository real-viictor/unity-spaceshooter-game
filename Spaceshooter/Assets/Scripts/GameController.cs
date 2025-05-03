using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Recebe os objetos dos inimigos
    [SerializeField] private GameObject enemy01Object;
    [SerializeField] private GameObject enemy02Object;

    //Recebe os objetos dos Power Ups
    [SerializeField] private GameObject shotPowerUpObject;
    [SerializeField] private GameObject healthPowerUpObject;

    //Variável do timer que controla quando o nível poderá começar
    private float levelStartTimer;

    //Variável do level atual
    [SerializeField] private int currentLevel;

    //Variável que controla quantos pontos precisa para passar de nível
    private int pointsToLevelUp;

    //Variável que controla quantos pontos o player tem
    [SerializeField] private int playerTotalPoints;

    //Variável que informa ao script se o nível já começou e se pode começar a spawnar inimigos
    private bool isLevelStarted;

    //Variáveis de range mínimo e máximo para Spawnar inimigos simultaneamente
    private int enemySpawnerMaxSimultaneousSpawns, enemySpawnerMinSimultaneousSpawns;

    //Variável que controla o timer do spawner de inimigos
    private float enemySpawnerTimer, enemySpawnerTimerMinRange, enemySpawnerTimerMaxRange;

    //Spawn rate do inimigo 2
    private float enemy02SpawnRate;

    //Informa ao Script se o boss pode nascer
    private bool canSpawnBoss;

    //Spawn Rate de cada criação de Power Ups
    private float shotPowerUpSpawnChance, healthPowerUpSpawnChance;

    //Chance de Spawn dos Power Ups
    private float powerUpSpawnChance;

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

    //Spawner de inimigos
    private void SpawnEnemies()
    {
        //Se o timer para começar o proximo nivel zerou, então comece a spawnar inimigos
        if (isLevelStarted)
        {
            //Caso o boss possa aparecer (ou seja, já é o nível 4), então spawne ele
            if(canSpawnBoss)
            {
                SpawnBoss();
            } else
            {
                //Senão, Spawne inimigos comuns
                SpawnCommomEnemies();
            }
        }
    }

    //Spawna inimigos comuns caso o boss não possa aparecer ainda
    private void SpawnCommomEnemies()
    {
        //reduz o timer de spawn até zero
        enemySpawnerTimer -= Time.deltaTime;

        //Ao zerar, redefine o timer e spawna um inimigo
        if (enemySpawnerTimer <= 0)
        {
            //Redefinindo o timer
            enemySpawnerTimer = Random.Range(enemySpawnerTimerMinRange, enemySpawnerTimerMaxRange);

            //Aleatorizando quantos inimigos nascerão de uma vez
            int simultaneousSpawns = Random.Range(enemySpawnerMinSimultaneousSpawns, enemySpawnerMaxSimultaneousSpawns + 1);

            //Dependendo de quantos inimigos devem ser spawnados por vez, execute esse comando em loop até a quantidade determinada pela variável anterior
            for (int i = 0; i < simultaneousSpawns; i++)
            {
                //Variável que randomiza um número de 0 a 100, que é a chance de spawnar o inimigo 2 ao invés do 1
                float spawnChance = Random.Range(0f, 1f);

                //Se o número aleatorizado estiver fora do range do Spawn Rate do inimigo 2, Spawne o inimigo 1
                if (spawnChance > enemy02SpawnRate)
                {
                    Instantiate(enemy01Object, new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 13f), 0), transform.rotation);
                }
                //Senão spawne o inimigo 2
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

    //Mudando as variáveis de dificuldade do jogo por nível
    private void ChangeLevelDifficult()
    {
        switch (currentLevel)
        {
            case 1:
                pointsToLevelUp = 100;
                enemySpawnerTimerMaxRange = 4;
                enemySpawnerTimerMinRange = 2;
                enemySpawnerMaxSimultaneousSpawns = 2;
                enemySpawnerMinSimultaneousSpawns = 1;
                enemy02SpawnRate = 0f;
                shotPowerUpSpawnChance = 0f;
                healthPowerUpSpawnChance = 0f;
                break;
            case 2:
                pointsToLevelUp = 250;
                enemySpawnerTimerMaxRange = 3;
                enemySpawnerTimerMinRange = 1;
                enemySpawnerMaxSimultaneousSpawns = 3;
                enemySpawnerMinSimultaneousSpawns = 1;
                enemy02SpawnRate = 0.15f;
                shotPowerUpSpawnChance = 0.1f;
                healthPowerUpSpawnChance = 0.05f;
                break;
            case 3:
                pointsToLevelUp = 500;
                enemySpawnerTimerMaxRange = 3;
                enemySpawnerTimerMinRange = 0.5f;
                enemySpawnerMaxSimultaneousSpawns = 6;
                enemySpawnerMinSimultaneousSpawns = 2;
                enemy02SpawnRate = 0.3f;
                shotPowerUpSpawnChance = 0.3f;
                healthPowerUpSpawnChance = 0.2f;
                break;
            case 4:
                canSpawnBoss = true;
                break;
        }
    }
    //Função que passa o player de nível ao atingir a pontuação necessária
    private void LevelUp()
    {
        //Se o player conseguiu pontos suficientes para passar de nível E ainda não é o nível do boss, então aumente 1 nível
        if (playerTotalPoints >= pointsToLevelUp && currentLevel < 4)
        {
            currentLevel++;
            //Redefina o timer para aguardar um pouco antes de iniciar o próximo nível
            levelStartTimer = 5;
            //Informe que o novo level ainda não começou
            isLevelStarted = false;

            //Mude as dificuldades do nível baseado no novo nível atingido
            ChangeLevelDifficult();
        }

        //Quando o timer zerar, diga que o nível começou, então o jogo começará a Spawnar inimigos
        if(levelStartTimer <= 0)
        {
            isLevelStarted = true;
        } //Caso contrário, reduza o timer até ele chegar em 0
        else
        {
            levelStartTimer -= Time.deltaTime;
        }
    }

    //Função que adiciona pontos, roda quando um inimigo morre e o parâmetro é o valor de pontos que cada tipo de inimigo dá ao morrer
    public void AddPoints(int points)
    {
        playerTotalPoints += points;
    }

    //Informa aos inimigos se o Boss pode vir, para que eles rodem a função de sair da tela
    public bool getBossSpawnCondition()
    {
        return canSpawnBoss;
    }

    public void SpawnPowerUps(Vector3 position)
    { 
        powerUpSpawnChance = Random.Range(0f, 1f);

        if(powerUpSpawnChance <= healthPowerUpSpawnChance)
        {
            Instantiate(healthPowerUpObject, position, Quaternion.identity);
        } else if (powerUpSpawnChance <= shotPowerUpSpawnChance)
        {
            Instantiate(shotPowerUpObject, position, Quaternion.identity);
        }
    }
}
