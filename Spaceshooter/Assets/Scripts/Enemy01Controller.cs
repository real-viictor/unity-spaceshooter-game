using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enemy01Controller : MonoBehaviour
{
    private Rigidbody2D enemyRB;

    //Velocidade padr�o do inimigo
    private float enemySpeed = 1.5f;

    //Vari�vel usada como timer que calcula o tempo de cada disparo
    private float shotTimer;

    //Vari�vel que guarda o tempo do disparo
    private float shotDelay;

    //Vari�vel de controle que informa ao script se o inimigo deve parar de se mover
    private bool isInPosition = false;

    //Vari�vel de vida que reduz a cada tiro tomado
    private int enemyHealth = 1;
    
    //Vari�vel que guardar� onde o inimigo ficar� posicionado na tela
    private float enemyPosition;

    //Vari�vel que determina onde o tiro deve sair
    [SerializeField] private Transform shotPosition;

    //Objeto do tiro, que � instanciado na fun��o Shoot()
    [SerializeField] private GameObject enemyShotObject;

    //Vari�vel que guarda o prefab de explos�o do inimigo
    [SerializeField] private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimenta��o do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotDelay = Random.Range(1f, 3f);

        //Atribuindo tempo do delay ao primeiro disparo do inimigo
        shotTimer = shotDelay;

        //Determinando a posi��o que o inimigo deve parar, para fim de efeito visual de um alinhamento menos preciso na horda de inimigos
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
        //Parando o inimigo na posi��o determinada da tela
        if (transform.position.y <= enemyPosition)
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
                shotDelay = Random.Range(2, 3);
                shotTimer = shotDelay;
                Instantiate(enemyShotObject, shotPosition.position, Quaternion.identity);
            }
        }
    }

    //Fun��o publica que causa dano ao inimigo, baseado no dano passado na chamada da vari�vel, caso a vida zere, o inimigo � destru�do
    public void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        enemyHealth-=damage;

        //Se o inimigo zerar a vida, instancie a explos�o no local onde ele estava e destrua o inimigo
        if (enemyHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
