using UnityEngine;

public class Entity : MonoBehaviour
{
    //Variável que guarda a vida da entidade
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

    //Função que causa dano na entidade
    public virtual void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        entityHealth -= damage;

        //Se o inimigo zerar a vida, instancie a explosão no local onde ele estava e destrua o inimigo
        if (entityHealth <= 0)
        {
            DestroyEntity();
        }
    }

    private void DestroyEntity()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
