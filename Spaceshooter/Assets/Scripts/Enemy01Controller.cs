using UnityEngine;

public class Enemy01Controller : EnemyEntity
{
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);

        //Determinando a posição que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        TakePosition();
        Shoot();
        GoAway();
    }

    //Função que posiciona o inimigo na altura da tela desejada
    private void TakePosition()
    {
        //Parando o inimigo na posição determinada da tela
        if (transform.position.y <= enemyTargetYPosition)
        {
            enemyRB.velocity = Vector2.zero;
            //Informando a variável de controle que o inimigo pode começar a atirar, pois já se posicionou
            canShoot = true;
        }
    }

    //Atira quando posicionado
    private void Shoot()
    {
        //Se o inimigo já chegou no local da tela desejado, então comece a contagem do timer
        if(canShoot && !gameController.getBossSpawnCondition())
        {
            //Reduzindo o Timer até zerar
            shotTimer -= Time.deltaTime;

            //Ao zerar o Timer, aleatoriza o tempo do próximo tiro, redefine o timer e instancia um tiro
            if(shotTimer <= 0)
            {
                CreateShot(enemyShotObject, shotPosition, Vector2.down, shotSpeed);
                shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);
            }
        }
    }
}
