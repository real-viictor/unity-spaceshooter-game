using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int entityHealth;

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
    public virtual void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        entityHealth -= damage;

        //Se o inimigo zerar a vida, instancie a explosão no local onde ele estava e destrua o inimigo
        if (entityHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
