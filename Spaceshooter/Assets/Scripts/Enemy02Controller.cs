using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    //Variável que guarda a curva de animação de Spawn, que faz a nave aparecer subitamente e ir reduzindo até o momento desejado
    [SerializeField] private AnimationCurve spawnAnimationCurve;

    //Variável que guarda a posição inicial do inimigo ao spawnar
    private Vector3 spawnAnimationStartPosition;

    //Variável que guarda a posição final desejada de Spawn que o inimigo deve parar
    private Vector3 spawnAnimationEndPosition;

    //Variável que determina a duração da animação de Spawn
    private float spawnAnimationDuration = 2f;

    //Variável que é usada para calcular o tempo que a animação de Spawn está durando 
    private float spawnAnimationElapsedTime;

    //Duração total da animação de movimentação antes do disparo
    private float movementAnimationDuration;

    //Variável que guarda o tempo decorrido da animação de se mover para atirar
    private float movementAnimationElapsedTime;

    //Vector3 que guarda a posição inicial do inimigo ao se mover para atirar 
    private Vector3 movementStartPosition;

    //Variável que controlará a posição alvo que o inimigo deverá chegar ao se movimentar antes de trocar de direção
    private float enemyTargetXPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Passando a posição de spawn como a posição inicial da animação
        spawnAnimationStartPosition = transform.position;

        //Determinando a posição que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(0.5f, 4.5f);

        //Definindo o primeiro lado que o inimigo vai se mover
        enemyTargetXPosition = 8f * (Random.Range(0, 2) * 2 - 1);

        //Passando a variável randomizada como posição final do inimigo, junto com o X onde ele spawnou
        spawnAnimationEndPosition = new Vector3(transform.position.x, enemyTargetYPosition);

        //Definindo a velocidade da movimentação do inimigo quando ele começar a atirar
        movementAnimationDuration = Random.Range(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        Shoot();
    }

    //Parando o inimigo e permitindo que ele comece a atirar
    private void Stop()
    {
        //Quando o inimigo estiver na posição desejada, acione a variável de controle que informa que ele pode atirar
        if (transform.position.y == enemyTargetYPosition)
        {
            isInPosition = true;
        }
        //Caso contrário, realize a animação de entrada
        else
        {
            //Somando o deltaTime ao tempo decorrido da animação
            spawnAnimationElapsedTime += Time.deltaTime;

            //Variável que guarda o % que a animação já está, baseada no tempo que levou / tempo máximo
            float spawnAnimationPercentage = spawnAnimationElapsedTime / spawnAnimationDuration;

            //Movendo o player usando Lerp, de maneira suave, respeitando a curva de animação definida baseada no decorrer da animação
            transform.position = Vector3.Lerp(spawnAnimationStartPosition, spawnAnimationEndPosition, spawnAnimationCurve.Evaluate(spawnAnimationPercentage));
        }
    }

    //Quando em posição, se movimentar para os lados e atirar toda vez que chegar no extremo da tela
    private void Shoot()
    {
        //Quando o inimigo estiver na posição certa, comece a movimentação e os disparos
        if (isInPosition)
        {
            //Caso o vetor que determina o início do movimento esteja zerado, atribua a posição atual do inimigo
            if(movementStartPosition == Vector3.zero)
            {
                movementStartPosition = transform.position;
            }

            //Aumentando o tempo decorrido com deltaTime
            movementAnimationElapsedTime += Time.deltaTime;
            
            //Calculando a porcentagem do decorrer da animação
            float movementAnimationPercentage = movementAnimationElapsedTime / movementAnimationDuration;

            //Movimentando o inimigo utilizando lerp para suavizar o movimento
            transform.position = Vector3.Lerp(movementStartPosition, new Vector3(enemyTargetXPosition, transform.position.y), Mathf.SmoothStep(0, 1, movementAnimationPercentage));

            //Se o inimigo chegou no canto do mapa, então faça-o ir para outro
            if (transform.position.x == enemyTargetXPosition)
            {
                //invertendo o sinal de X para ir ao outro extremo do mapa
                enemyTargetXPosition *= -1;
                //Zerando o tempo decorrido da animação
                movementAnimationElapsedTime = 0;
                //Redefinindo o novo ponto de partida do inimigo
                movementStartPosition = transform.position;
                //Aleatorizando o tempo que a movimentação durará
                movementAnimationDuration = Random.Range(3f, 5f);
                //Atirando
                Instantiate(enemyShotObject, transform.position, transform.rotation);
            }
        }
    }
}
