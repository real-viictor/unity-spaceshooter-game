using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Recebe os objetos dos inimigos
    [SerializeField] private GameObject enemy01Object;
    [SerializeField] private GameObject enemy02Object;

    //Vari�vel do timer que controla quando o n�vel poder� come�ar
    private float levelStartTimer;

    //Vari�vel do level atual
    [SerializeField] private int currentLevel;

    //Vari�vel que controla quantos pontos precisa para passar de n�vel
    private int pointsToLevelUp;

    //Vari�vel que controla quantos pontos o player tem
    [SerializeField] private int playerTotalPoints;

    //Vari�vel que informa ao script se o n�vel j� come�ou e se pode come�ar a spawnar inimigos
    private bool isLevelStarted;

    //Vari�veis de range m�nimo e m�ximo para Spawnar inimigos simultaneamente
    private int spawnerMaxSimultaneousSpawns;
    private int spawnerMinSimultaneousSpawns;

    //Vari�vel que controla o timer do spawner de inimigos
    private float spawnerTimer;
    private float spawnerTimerMinRange;
    private float spawnerTimerMaxRange;

    //Spawn rate do inimigo 2
    private float enemy02SpawnRate;

    //Informa ao Script se o boss pode nascer
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

    //Spawner de inimigos
    private void SpawnEnemies()
    {
        //Se o timer para come�ar o proximo nivel zerou, ent�o comece a spawnar inimigos
        if (isLevelStarted)
        {
            //Caso o boss possa aparecer (ou seja, j� � o n�vel 4), ent�o spawne ele
            if(canSpawnBoss)
            {
                SpawnBoss();
            } else
            {
                //Sen�o, Spawne inimigos comuns
                SpawnCommomEnemies();
            }
        }
    }

    //Spawna inimigos comuns caso o boss n�o possa aparecer ainda
    private void SpawnCommomEnemies()
    {
        //reduz o timer de spawn at� zero
        spawnerTimer -= Time.deltaTime;

        //Ao zerar, redefine o timer e spawna um inimigo
        if (spawnerTimer <= 0)
        {
            //Redefinindo o timer
            spawnerTimer = Random.Range(spawnerTimerMinRange, spawnerTimerMaxRange);

            //Aleatorizando quantos inimigos nascer�o de uma vez
            int simultaneousSpawns = Random.Range(spawnerMinSimultaneousSpawns, spawnerMaxSimultaneousSpawns + 1);

            //Dependendo de quantos inimigos devem ser spawnados por vez, execute esse comando em loop at� a quantidade determinada pela vari�vel anterior
            for (int i = 0; i < simultaneousSpawns; i++)
            {
                //Vari�vel que randomiza um n�mero de 0 a 100, que � a chance de spawnar o inimigo 2 ao inv�s do 1
                float spawnChance = Random.Range(0f, 1f);

                //Se o n�mero aleatorizado estiver fora do range do Spawn Rate do inimigo 2, Spawne o inimigo 1
                if (spawnChance > enemy02SpawnRate)
                {
                    Instantiate(enemy01Object, new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 13f), 0), transform.rotation);
                }
                //Sen�o spawne o inimigo 2
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

    //Mudando as vari�veis de dificuldade do jogo por n�vel
    private void ChangeLevelDifficult()
    {
        switch (currentLevel)
        {
            case 1:
                pointsToLevelUp = 100;
                spawnerTimerMaxRange = 4;
                spawnerTimerMinRange = 2;
                spawnerMaxSimultaneousSpawns = 2;
                spawnerMinSimultaneousSpawns = 1;
                break;
            case 2:
                pointsToLevelUp = 250;
                spawnerTimerMaxRange = 3;
                spawnerTimerMinRange = 1;
                spawnerMaxSimultaneousSpawns = 3;
                spawnerMinSimultaneousSpawns = 1;
                enemy02SpawnRate = 0.15f;
                break;
            case 3:
                pointsToLevelUp = 500;
                spawnerTimerMaxRange = 3;
                spawnerTimerMinRange = 0.5f;
                spawnerMaxSimultaneousSpawns = 6;
                spawnerMinSimultaneousSpawns = 2;
                enemy02SpawnRate = 0.25f;
                break;
            case 4:
                canSpawnBoss = true;
                break;
        }
    }
    //Fun��o que passa o player de n�vel ao atingir a pontua��o necess�ria
    private void LevelUp()
    {
        //Se o player conseguiu pontos suficientes para passar de n�vel E ainda n�o � o n�vel do boss, ent�o aumente 1 n�vel
        if (playerTotalPoints >= pointsToLevelUp && currentLevel < 4)
        {
            currentLevel++;
            //Redefina o timer para aguardar um pouco antes de iniciar o pr�ximo n�vel
            levelStartTimer = 5;
            //Informe que o novo level ainda n�o come�ou
            isLevelStarted = false;

            //Mude as dificuldades do n�vel baseado no novo n�vel atingido
            ChangeLevelDifficult();
        }

        //Quando o timer zerar, diga que o n�vel come�ou, ent�o o jogo come�ar� a Spawnar inimigos
        if(levelStartTimer <= 0)
        {
            isLevelStarted = true;
        } //Caso contr�rio, reduza o timer at� ele chegar em 0
        else
        {
            levelStartTimer -= Time.deltaTime;
        }
    }

    //Fun��o que adiciona pontos, roda quando um inimigo morre e o par�metro � o valor de pontos que cada tipo de inimigo d� ao morrer
    public void AddPoints(int points)
    {
        playerTotalPoints += points;
    }

    //Informa aos inimigos se o Boss pode vir, para que eles rodem a fun��o de sair da tela
    public bool getBossSpawnCondition()
    {
        return canSpawnBoss;
    }
}
