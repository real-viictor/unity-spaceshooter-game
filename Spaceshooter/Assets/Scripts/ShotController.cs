using UnityEngine;

public class ShotController : MonoBehaviour
{   
    [SerializeField] private int shotDamage = 1;

    [SerializeField] private GameObject shotExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenOutside();
    }

    //Se destruindo ao sair da tela
    private void DestroyWhenOutside()
    {
        //Vendo se o SpriteRenderer do filho não é visível, se não é visível, então está fora da tela, logo destrua-se
        if (!GetComponentInChildren<SpriteRenderer>().isVisible) {
            Destroy(gameObject);
        }
    }

    //Causando dano na entidade que o tiro localizar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Entity>())
        {
            collision.gameObject.GetComponent<Entity>().LoseHealth(shotDamage);
            //Alterando a Sprite do Shield quando ele é acertado por um tiro
            if(collision.CompareTag("Shield"))
            {
                collision.gameObject.GetComponent<ShieldController>().UpdateSprite();
            }
            Instantiate(shotExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
