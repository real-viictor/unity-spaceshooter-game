using UnityEngine;
using UnityEngine.UIElements;

public class Enemy02Controller : EnemyEntity
{
    //Vari�vel que controla a dire��o
    private int movementDirection;

    //Vari�vel que controla para que lado o inimigo est� indo
    private bool isMovingLeft;

    //Vari�vel que diz para o inimigo at� onde ele deve ir
    private int enemyTargetXPosition = 8;

    private PlayerController enemyShotTarget;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        enemyShotTarget = FindObjectOfType<PlayerController>();

        //Determinando velocidade do inimigo
        enemyRB.velocity = Vector3.down * enemySpeed;

        //Determinando a posi��o que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
        enemyTargetYPosition = Random.Range(0.5f, 4.5f);

        //Determinando o lado que o inimigo ir� inicialmente
        movementDirection = Random.Range(0, 2) * 2 - 1;

        isMovingLeft = movementDirection == -1 ? true : false;

        //Aleatorizando o primeiro tiro
        shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);
    }

    // Update is called once per frame
    void Update()
    {
        TakePosition();
        MoveAround();
        Shoot();
        GoAway();
    }

    //Parando o inimigo e permitindo que ele comece a atirar
    private void TakePosition()
    {
        //Se ele chegou na altura desejada, ent�o zere a velocidade dele e informe que ele pode come�ar a atirar
        if (transform.position.y <= enemyTargetYPosition)
        {
            enemyRB.velocity = Vector3.zero;
            canShoot = true;
        }
    }


    //Fun��o que move o inimigo de um lado para o outro
    private void MoveAround()
    {
        //Se o inimigo j� pode atirar, ent�o permita que ele comece a se mover para os lados
        if (canShoot)
        {
            //Determinando se o inimigo est� indo para a esquerda ou para a direita
            if (transform.position.x <= -enemyTargetXPosition) isMovingLeft = false;
            else if (transform.position.x >= enemyTargetXPosition) isMovingLeft = true;

            //Mudando de dire��o dependendo da dire��o que ele vai
            if (isMovingLeft)
            {
                movementDirection = 1;
            }
            else
            {
                movementDirection = -1;
            }

            //Atualizando a velocidade do inimigo para mud�-lo de dire��o
            enemyRB.velocity = Vector2.left * enemySpeed * movementDirection;
        }
    }


    //Atirando quando zerar o timer
    private void Shoot()
    {
        //Se o inimigo pode atirar e o boss n�o vai Spawnar, ent�o execute o tiro
        if (canShoot && !gameController.getBossSpawnCondition())
        {
            //Zerando o timer de delay do tiro
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0)
            {
                //Ao zerar, redefina o tiro e execute a a��o de atirar
                shotTimer = Random.Range(shotTimerMinRange, shotTimerMinRange + 1);

                //Atirando apenas se o inimigo conseguiu localizar o player na cena
                if (enemyShotTarget != null)
                {
                    //Calculando a dire��o que o tiro deve ir
                    Vector2 shotDirection = enemyShotTarget.transform.position - shotPosition.transform.position;

                    //Fazendo o tiro "olhar" para o Player
                    //NOTE: A soma de +90 � para compensar a Sprite
                    float shotRotation = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg + 90;

                    CreateShot(enemyShotObject, shotPosition, shotDirection.normalized, shotSpeed, shotRotation);
                }
            }
        }   
    }
}