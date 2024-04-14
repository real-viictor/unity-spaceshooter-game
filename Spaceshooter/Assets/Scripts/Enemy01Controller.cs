using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enemy01Controller : EnemyEntity
{
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimenta��o do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);

        //Determinando a posi��o que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        Shoot();
        GoAway();
    }

    //Para o inimigo na altura da tela desejada
    private void Spawn()
    {
        //Parando o inimigo na posi��o determinada da tela
        if (transform.position.y <= enemyTargetYPosition)
        {
            enemyRB.velocity = Vector2.zero;
            //Informando a vari�vel de controle que o inimigo pode come�ar a atirar, pois j� se posicionou
            canShoot = true;
        }
    }

    //Atira quando posicionado
    private void Shoot()
    {
        //Se o inimigo j� chegou no local da tela desejado, ent�o comece a contagem do timer
        if(canShoot && !gameController.getBossSpawnCondition())
        {
            //Reduzindo o Timer at� zerar
            shotTimer -= Time.deltaTime;

            //Ao zerar o Timer, aleatoriza o tempo do pr�ximo tiro, redefine o timer e instancia um tiro
            if(shotTimer <= 0)
            {
                shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);

                //Salvando a inst�ncia do tiro em uma vari�vel
                GameObject shotInstance = Instantiate(enemyShotObject, shotPosition.position, Quaternion.identity);

                //Determinando a velocidade e dire��o do tiro
                shotInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * shotSpeed;
            }
        }
    }
}
