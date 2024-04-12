using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enemy01Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    //Variável usada como timer que calcula o tempo de cada disparo
    private float shotTimer;

    //Variável que determina onde o tiro deve sair
    [SerializeField] private Transform shotPosition;

    //Objeto do tiro, que é instanciado na função Shoot()
    [SerializeField] private GameObject enemyShotObject;

    //Variável de controle que informa ao script se o inimigo deve parar de se mover
    private bool isInPosition = false;
    
    //Variável que guardará onde o inimigo ficará posicionado na tela
    private float enemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(1f, 3f);

        //Determinando a posição que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyPosition = Random.Range(2f, 4f);
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
        if (transform.position.y <= enemyPosition)
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
