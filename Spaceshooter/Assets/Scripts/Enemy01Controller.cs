using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enemy01Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(1f, 3f);

        //Determinando a posição que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        Shoot();
    }

    //Para o inimigo na altura da tela desejada
    private void Stop()
    {
        //Parando o inimigo na posição determinada da tela
        if (transform.position.y <= enemyTargetYPosition)
        {
            enemyRB.velocity = Vector2.down * 0;
            //Informando a variável de controle que o inimigo pode começar a atirar, pois já se posicionou
            isInPosition = true;           
        }
    }

    //Atira quando posicionado
    private void Shoot()
    {
        //Se o inimigo já chegou no local da tela desejado, então comece a contagem do timer
        if(isInPosition)
        {
            //Reduzindo o Timer até zerar
            shotTimer -= Time.deltaTime;

            //Ao zerar o Timer, aleatoriza o tempo do próximo tiro, redefine o timer e instancia um tiro
            if(shotTimer <= 0)
            {
                shotTimer = Random.Range(2, 3);
                Instantiate(enemyShotObject, shotPosition.position, Quaternion.identity);
            }
        }
    }

    //Função publica que causa dano ao inimigo, baseado no dano passado na chamada da variável, caso a vida zere, o inimigo é destruído
    
}
