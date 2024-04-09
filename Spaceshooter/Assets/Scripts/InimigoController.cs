using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    private Rigidbody2D enemyRB;

    //Velocidade padrão do inimigo
    private float enemySpeed = 1.5f;

    //Variável usada como timer que calcula o tempo de cada disparo
    private float shotTimer;

    //Variável que guarda o tempo do disparo
    private float shotDelay;

    //Variável de controle que informa ao script se o inimigo deve parar de se mover
    private bool isInPosition = false;

    //Variável que determina onde o tiro deve sair
    [SerializeField] private Transform shotPosition;

    //Objeto do tiro, que é instanciado na função Shoot()
    [SerializeField] private GameObject enemyShotObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;

        //Aleatorizando o primeiro tiro
        shotDelay = Random.Range(1f, 3f);

        //Atribuindo tempo do delay ao primeiro disparo do inimigo
        shotTimer = shotDelay;
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
        //Parando o inimigo na posição desejada da tela
        if (transform.position.y <= 3.5f)
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
                shotDelay = Random.Range(2, 3);
                shotTimer = shotDelay;
                Instantiate(enemyShotObject, shotPosition.position, Quaternion.identity);
            }
        }
    }
}
