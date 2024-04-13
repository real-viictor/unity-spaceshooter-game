using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    //Variável usada como timer que calcula o tempo de cada disparo
    protected float shotTimer;

    [SerializeField] protected float shotSpeed;

    //Variável que determina onde o tiro deve sair
    [SerializeField] protected Transform shotPosition;

    //Objeto do tiro, que é instanciado na função Shoot()
    [SerializeField] protected GameObject enemyShotObject;

    //Variável que guarda o prefab de explosão do inimigo
    [SerializeField] protected GameObject explosion;

    //Velocidade padrão do inimigo
    [SerializeField] protected float enemySpeed;

    //Variável de vida que reduz a cada tiro tomado
    [SerializeField] protected int enemyHealth;

    //Variável de controle que informa ao script se o inimigo deve parar de se mover
    protected bool isInPosition = false;

    //Variável que guardará onde o inimigo ficará posicionado na tela
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

        //Se o inimigo zerar a vida, instancie a explosão no local onde ele estava e destrua o inimigo
        if (enemyHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
