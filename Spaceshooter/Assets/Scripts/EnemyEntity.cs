using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    //Vari�vel usada como timer que calcula o tempo de cada disparo
    protected float shotTimer;

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
    protected bool isInPosition = false;

    //Vari�vel que guardar� onde o inimigo ficar� posicionado na tela
    protected float enemyTargetYPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        enemyHealth -= damage;

        //Se o inimigo zerar a vida, instancie a explos�o no local onde ele estava e destrua o inimigo
        if (enemyHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
