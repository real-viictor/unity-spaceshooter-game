using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    //Vari�vel que guarda a curva de anima��o de Spawn, que faz a nave aparecer subitamente e ir reduzindo at� o momento desejado
    [SerializeField] private AnimationCurve spawnAnimationCurve;

    //Vari�vel que guarda a posi��o inicial do inimigo ao spawnar
    private Vector3 spawnAnimationStartPosition;

    //Vari�vel que guarda a posi��o final desejada de Spawn que o inimigo deve parar
    private Vector3 spawnAnimationEndPosition;

    //Vari�vel que determina a dura��o da anima��o de Spawn
    private float spawnAnimationDuration = 2f;

    //Vari�vel que � usada para calcular o tempo que a anima��o de Spawn est� durando 
    private float spawnAnimationElapsedTime;

    //Dura��o total da anima��o de movimenta��o antes do disparo
    private float movementAnimationDuration;

    //Vari�vel que guarda o tempo decorrido da anima��o de se mover para atirar
    private float movementAnimationElapsedTime;

    //Vector3 que guarda a posi��o inicial do inimigo ao se mover para atirar 
    private Vector3 movementStartPosition;

    //Vari�vel que controlar� a posi��o alvo que o inimigo dever� chegar ao se movimentar antes de trocar de dire��o
    private float enemyTargetXPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Passando a posi��o de spawn como a posi��o inicial da anima��o
        spawnAnimationStartPosition = transform.position;

        //Determinando a posi��o que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(0.5f, 4.5f);

        //Definindo o primeiro lado que o inimigo vai se mover
        enemyTargetXPosition = 8f * (Random.Range(0, 2) * 2 - 1);

        //Passando a vari�vel randomizada como posi��o final do inimigo, junto com o X onde ele spawnou
        spawnAnimationEndPosition = new Vector3(transform.position.x, enemyTargetYPosition);

        //Definindo a velocidade da movimenta��o do inimigo quando ele come�ar a atirar
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
        //Quando o inimigo estiver na posi��o desejada, acione a vari�vel de controle que informa que ele pode atirar
        if (transform.position.y == enemyTargetYPosition)
        {
            isInPosition = true;
        }
        //Caso contr�rio, realize a anima��o de entrada
        else
        {
            //Somando o deltaTime ao tempo decorrido da anima��o
            spawnAnimationElapsedTime += Time.deltaTime;

            //Vari�vel que guarda o % que a anima��o j� est�, baseada no tempo que levou / tempo m�ximo
            float spawnAnimationPercentage = spawnAnimationElapsedTime / spawnAnimationDuration;

            //Movendo o player usando Lerp, de maneira suave, respeitando a curva de anima��o definida baseada no decorrer da anima��o
            transform.position = Vector3.Lerp(spawnAnimationStartPosition, spawnAnimationEndPosition, spawnAnimationCurve.Evaluate(spawnAnimationPercentage));
        }
    }

    //Quando em posi��o, se movimentar para os lados e atirar toda vez que chegar no extremo da tela
    private void Shoot()
    {
        //Quando o inimigo estiver na posi��o certa, comece a movimenta��o e os disparos
        if (isInPosition)
        {
            //Caso o vetor que determina o in�cio do movimento esteja zerado, atribua a posi��o atual do inimigo
            if(movementStartPosition == Vector3.zero)
            {
                movementStartPosition = transform.position;
            }

            //Aumentando o tempo decorrido com deltaTime
            movementAnimationElapsedTime += Time.deltaTime;
            
            //Calculando a porcentagem do decorrer da anima��o
            float movementAnimationPercentage = movementAnimationElapsedTime / movementAnimationDuration;

            //Movimentando o inimigo utilizando lerp para suavizar o movimento
            transform.position = Vector3.Lerp(movementStartPosition, new Vector3(enemyTargetXPosition, transform.position.y), Mathf.SmoothStep(0, 1, movementAnimationPercentage));

            //Se o inimigo chegou no canto do mapa, ent�o fa�a-o ir para outro
            if (transform.position.x == enemyTargetXPosition)
            {
                //invertendo o sinal de X para ir ao outro extremo do mapa
                enemyTargetXPosition *= -1;
                //Zerando o tempo decorrido da anima��o
                movementAnimationElapsedTime = 0;
                //Redefinindo o novo ponto de partida do inimigo
                movementStartPosition = transform.position;
                //Aleatorizando o tempo que a movimenta��o durar�
                movementAnimationDuration = Random.Range(3f, 5f);
                //Atirando
                Instantiate(enemyShotObject, transform.position, transform.rotation);
            }
        }
    }
}
