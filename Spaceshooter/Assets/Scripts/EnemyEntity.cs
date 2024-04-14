using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    //Vari�vel usada como timer que calcula o tempo de cada disparo
    protected float shotTimer;

    //Vari�vel para pegar o RB dos inimigos, herdada do pai
    protected Rigidbody2D enemyRB;

    //Vari�vel para pegar o gameController do jogo, herdada do pai
    protected GameController gameController;

    //Intervalo m�nimo do disparo do inimigo
    [SerializeField] protected int shotTimerMinRange;

    //Intervalo m�ximo do disparo do inimigo
    [SerializeField] protected int shotTimerMaxRange;

    //Pontos que o inimigo dar� ao morrer
    [SerializeField] protected int points;

    //Velocidade do tiro
    [SerializeField] protected float shotSpeed;

    //Vari�vel que determina onde o tiro deve sair
    [SerializeField] protected Transform shotPosition;

    //Objeto do tiro, que � instanciado na fun��o Shoot()
    [SerializeField] protected GameObject enemyShotObject;

    //Vari�vel que guarda o prefab de explos�o do inimigo
    [SerializeField] protected GameObject explosion;

    //Velocidade padr�o do inimigo
    [SerializeField] protected float enemySpeed;

    //Vari�vel de vida que reduz a cada tiro tomado
    [SerializeField] protected int enemyHealth;

    //Vari�vel de controle que informa ao script se o inimigo deve parar de se mover
    protected bool canShoot = false;

    //Vari�vel que guardar� onde o inimigo ficar� posicionado na tela
    protected float enemyTargetYPosition;

    protected int goAwayDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Fun��o que tira os inimigos vivos da tela quando o boss estiver chegando
    protected void GoAway()
    {
        //Verificando se o boss pode vir
        if (gameController.getBossSpawnCondition())
        {
            //Setando o valor da vari�vel apenas uma vez
            if (goAwayDirection == 0)
            {
                //Determinando o lado que o inimigo sair� da tela baseado em onde ele est�
                goAwayDirection = transform.position.x <= 0 ? 1 : -1;
            }

            //Impedindo o inimigo de atirar enquanto sai
            canShoot = false;

            //Fazendo a velocidade dele ser lateral e ir para a dire��o mais pr�xima do fim da tela
            enemyRB.velocity = Vector2.left * enemySpeed * goAwayDirection;

            //Se o inimigo sair da �rea da tela, destrua ele
            if(!GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                Destroy(gameObject);
            }
        }
    }

    //Tirando vida do inimigo e matando-o se necess�rio
    public void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        enemyHealth -= damage;

        //Se o inimigo zerar a vida, instancie a explos�o no local onde ele estava e destrua o inimigo
        if (enemyHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameController.AddPoints(points);
        }
    }
}
