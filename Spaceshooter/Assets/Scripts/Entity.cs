using UnityEngine;

public class Entity : MonoBehaviour
{
    //Vari�vel que guarda a vida da entidade
    [SerializeField] protected int entityHealth;

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

    //Fun��o que causa dano na entidade
    public virtual void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        entityHealth -= damage;

        //Se o inimigo zerar a vida, instancie a explos�o no local onde ele estava e destrua o inimigo
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
