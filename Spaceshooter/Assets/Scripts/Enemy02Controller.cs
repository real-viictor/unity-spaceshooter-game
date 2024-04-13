using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float movementDuration = 2f;
    private float movementElapsedTime;

    [SerializeField] private AnimationCurve movementAnimationCurve;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Determinando a posição que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(0.5f, 4.5f);

        startPosition = transform.position;

        endPosition = new Vector3(transform.position.x, enemyTargetYPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        Shoot();
    }

    private void Stop()
    { 
        if(transform.position.y == enemyTargetYPosition)
        {
            isInPosition = true;
        } else
        {
            movementElapsedTime += Time.deltaTime;
            float movementPercentage = movementElapsedTime / movementDuration;

            transform.position = Vector3.Lerp(startPosition, endPosition, movementAnimationCurve.Evaluate(movementPercentage));
        }
    }

    private void Shoot()
    {

    }
}
