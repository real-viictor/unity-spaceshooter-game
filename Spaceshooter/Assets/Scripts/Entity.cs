using UnityEngine;

public class Entity : MonoBehaviour
{
    //Variável que guarda a vida da entidade
    [SerializeField] protected int entityHealth;

    //Variável que guarda o prefab de explosão do inimigo
    [SerializeField] protected GameObject explosion;

    [SerializeField] protected bool canBeHit;

    // Start is called before the first frame update
    void Start()
    {
        canBeHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Função que causa dano na entidade
    public virtual void LoseHealth(int damage)
    {
        //Reduzindo a vida baseado no dano
        if (canBeHit)
        {
            entityHealth -= damage;
        }

        //Se o inimigo zerar a vida, instancie a explosão no local onde ele estava e destrua o inimigo
        //Não aplicável ao Boss do Jogo
        if (entityHealth <= 0 && !CompareTag("Boss"))
        {
            DestroyEntity();
        }
    }

    private void DestroyEntity()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
    public void CreateShot(GameObject shotObject, Transform position, Vector2 direction, float speed, float rotation = 0f)
    {
        GameObject shotInstance = Instantiate(shotObject, position.position, Quaternion.identity);
        Rigidbody2D shotRB = shotInstance.GetComponent<Rigidbody2D>();
        shotRB.velocity = direction * speed;
        shotRB.rotation = rotation;
    }
    
}
