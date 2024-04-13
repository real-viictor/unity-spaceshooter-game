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

        //Definindo movimenta��o do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(1f, 3f);

        //Determinando a posi��o que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
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
        //Parando o inimigo na posi��o determinada da tela
        if (transform.position.y <= enemyTargetYPosition)
        {
            enemyRB.velocity = Vector2.down * 0;
            //Informando a vari�vel de controle que o inimigo pode come�ar a atirar, pois j� se posicionou
            isInPosition = true;           
        }
    }

    //Atira quando posicionado
    private void Shoot()
    {
        //Se o inimigo j� chegou no local da tela desejado, ent�o comece a contagem do timer
        if(isInPosition)
        {
            //Reduzindo o Timer at� zerar
            shotTimer -= Time.deltaTime;

            //Ao zerar o Timer, aleatoriza o tempo do pr�ximo tiro, redefine o timer e instancia um tiro
            if(shotTimer <= 0)
            {
                shotTimer = Random.Range(2, 3);
                GameObject shotInstance = Instantiate(enemyShotObject, shotPosition.position, Quaternion.identity);

                shotInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * shotSpeed;
            }
        }
    }

    //Fun��o publica que causa dano ao inimigo, baseado no dano passado na chamada da vari�vel, caso a vida zere, o inimigo � destru�do
    
}
