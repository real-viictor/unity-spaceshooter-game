using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{   
    [SerializeField] private int shotDamage = 1;

    [SerializeField] private GameObject shotExplosion;

    [SerializeField] private string origin;

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
            if (collision.CompareTag("Shield") && origin == "Player") return;

            collision.gameObject.GetComponent<Entity>().LoseHealth(shotDamage);
            Instantiate(shotExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
