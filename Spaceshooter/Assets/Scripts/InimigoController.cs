using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    private Rigidbody2D enemyRB;

    private float enemySpeed = 1.5f;

    private float shotTimer;

    private float shotDelay = 3f;

    private bool isInPosition = false;

    [SerializeField] private GameObject enemyShotObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimenta��o do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;
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
        //Parando o inimigo na posi��o desejada da tela
        if (transform.position.y <= 3.5f)
        {
            enemyRB.velocity = Vector2.down * 0;
            //Informando a vari�vel de controle que o inimigo pode come�ar a atirar, pois j� se posicionou
            isInPosition = true;

            //Definindo o tempo do primeiro disparo ap�s se posicionar
            shotTimer = shotDelay;
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

            //Ao zerar o Timer, redefine o timer e instancia um tiro
            if(shotTimer <= 0)
            {
                shotTimer = shotDelay;
                Instantiate(enemyShotObject, new Vector3(transform.position.x, transform.position.y - 0.3f, 0), Quaternion.identity);
            }
        }
    }
}
