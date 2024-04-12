using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Variável que controla o timer do spawner de inimigos
    private float spawnerTimer;

    //Recebe o objeto do inimigo
    [SerializeField] private GameObject enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        //Definindo o timer do primeiro Spawn
        spawnerTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    //Spawnando inimigos baseado em timer
    private void SpawnEnemies()
    {
        spawnerTimer -= Time.deltaTime;

        //Quando o timer zera, redefine o timer em um valor aleatório e instancia o inimigo em algum ponto da tela
         if (spawnerTimer <= 0)
         {
            spawnerTimer = Random.Range(1f, 3f);
            Instantiate(enemyObject, new Vector3(Random.Range(-8f,8f),8,0), Quaternion.identity);
         }   
    }
}
