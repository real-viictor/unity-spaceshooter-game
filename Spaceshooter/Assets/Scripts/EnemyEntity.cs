using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{

    //Velocidade padrão do inimigo
    protected float enemySpeed = 1.5f;

    //Variável de vida que reduz a cada tiro tomado
    protected int enemyHealth = 1;

    //Variável que guarda o prefab de explosão do inimigo
    [SerializeField] protected GameObject explosion;

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
