using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float spawnerTimer;

    [SerializeField] private GameObject enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        spawnerTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        spawnerTimer -= Time.deltaTime;
         if (spawnerTimer <= 0 )
        {
            spawnerTimer = Random.Range(0.5f, 2f);
            Instantiate(enemyObject, new Vector3(Random.Range(-8f,8f),8,0), Quaternion.identity);
        }
    }
}
