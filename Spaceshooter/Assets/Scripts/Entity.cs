using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    //Variável que guarda a vida da entidade
    [SerializeField] protected int entityHealth;

    //Variável que guarda o prefab de explosão do inimigo
    [SerializeField] protected GameObject explosion;

    [SerializeField] protected bool canBeHit;

    [SerializeField] protected TextMeshProUGUI healthField;

    //Variável para pegar o gameController do jogo
    protected GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        UpdateUIStats();
        gameController = FindObjectOfType<GameController>();
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

        UpdateUIStats();

        //Se o inimigo zerar a vida, instancie a explosão no local onde ele estava e destrua o inimigo
        //Não aplicável ao Boss do Jogo
        if (entityHealth <= 0)
        {
            DestroyEntity();
        }
    }

    protected bool isAtPosition(Vector2 currentPosition, Vector2 targetPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) < 0.01f;
    }

    private void DestroyEntity()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void UpdateUIStats()
    {
        if (healthField)
        {
            healthField.text = "x " + entityHealth.ToString();
        }
    }

    
    public void CreateShot(GameObject shotObject, Transform position, Vector2 direction, float speed, float rotation = 0f)
    {
        GameObject shotInstance = Instantiate(shotObject, position.position, Quaternion.identity);
        Rigidbody2D shotRB = shotInstance.GetComponent<Rigidbody2D>();
        shotRB.velocity = direction * speed;
        shotRB.rotation = rotation;
    }
    
}
