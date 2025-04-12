using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    //Variável usada como timer que calcula o tempo de cada disparo
    protected float shotTimer;

    //Variável para pegar o RB dos inimigos, herdada do pai
    protected Rigidbody2D enemyRB;

    //Variável para pegar o gameController do jogo, herdada do pai
    protected GameController gameController;

    //Intervalo mínimo do disparo do inimigo
    [SerializeField] protected int shotTimerMinRange;

    //Intervalo máximo do disparo do inimigo
    [SerializeField] protected int shotTimerMaxRange;

    //Pontos que o inimigo dará ao morrer
    [SerializeField] protected int points;

    //Velocidade do tiro
    [SerializeField] protected float shotSpeed;

    //Variável que determina onde o tiro deve sair
    [SerializeField] protected Transform shotPosition;

    //Objeto do tiro, que é instanciado na função Shoot()
    [SerializeField] protected GameObject enemyShotObject;

    //Velocidade padrão do inimigo
    [SerializeField] protected float enemySpeed;

    //Variável de vida que reduz a cada tiro tomado
    [SerializeField] protected int enemyHealth;

    //Variável de controle que informa ao script se o inimigo deve parar de se mover
    protected bool canShoot = false;

    //Variável que guardará onde o inimigo ficará posicionado na tela
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

    //Função que tira os inimigos vivos da tela quando o boss estiver chegando
    protected void GoAway()
    {
        //Verificando se o boss pode vir
        if (gameController.getBossSpawnCondition())
        {
            //Setando o valor da variável apenas uma vez
            if (goAwayDirection == 0)
            {
                //Determinando o lado que o inimigo sairá da tela baseado em onde ele está (Ele sairá sempre pelo lado mais próximo possível)
                goAwayDirection = transform.position.x <= 0 ? 1 : -1;
            }

            //Impedindo o inimigo de atirar enquanto sai
            canShoot = false;

            //Fazendo a velocidade dele ser lateral e ir para a direção mais próxima do fim da tela
            enemyRB.velocity = Vector2.left * enemySpeed * goAwayDirection;

            //Se o inimigo sair da área da tela, destrua ele
            if(!GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                Destroy(gameObject);
            }
        }
    }

    //Tirando vida do inimigo e matando-o se necessário
    public override void LoseHealth(int damage)
    {
        base.LoseHealth(damage);
        gameController.AddPoints(points);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MainPlayer"))
        {
            collision.gameObject.GetComponent<Entity>().LoseHealth(2);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
