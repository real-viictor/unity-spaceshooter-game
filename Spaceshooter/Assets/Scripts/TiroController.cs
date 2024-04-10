using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

    [SerializeField] private int shotDamage = 1;

    [SerializeField] private GameObject shotExplosion;

    //Variável que guarda o Rigidbody do Player
    private Rigidbody2D shotRB;

    // Start is called before the first frame update
    void Start()
    {
        //Acessando Rigidbody
        shotRB = GetComponent<Rigidbody2D>();

        //Movendo o tiro baseado na velocidade
        shotRB.velocity = Vector2.up * shotSpeed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(shotExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);

        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<InimigoController>().LoseHealth(shotDamage);
        }else
        {
            collision.gameObject.GetComponent<PlayerController>().LoseHealth(shotDamage);
        }
    }
}
