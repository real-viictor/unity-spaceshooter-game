using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{

    //Velocidade padr�o do inimigo
    protected float enemySpeed = 1.5f;

    //Vari�vel de vida que reduz a cada tiro tomado
    protected int enemyHealth = 1;

    //Vari�vel que guarda o prefab de explos�o do inimigo
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

        //Se o inimigo zerar a vida, instancie a explos�o no local onde ele estava e destrua o inimigo
        if (enemyHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
