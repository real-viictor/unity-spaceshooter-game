using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    private Rigidbody2D enemyRB;

    private float enemySpeed = 1.5f;

    private float shootInterval = 3f;

    private bool isInPosition = false;

    [SerializeField] private GameObject enemyShotObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        Shoot();
    }

    private void Stop()
    {
        //Parando o inimigo na posição desejada da tela
        if (transform.position.y <= 3.5f)
        {
            enemyRB.velocity = Vector2.down * 0;
            isInPosition = true;
        }
    }

    private void Shoot()
    {
        if(isInPosition)
        {
            shootInterval -= Time.deltaTime;
            if(shootInterval <= 0)
            {
                shootInterval = 3f;
                Instantiate(enemyShotObject, transform.position, Quaternion.identity);
            }
        }
    }
}
