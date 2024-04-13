using System.Collections;
using System.Collections.Generic;
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
        //Vendo se o SpriteRenderer do filho n�o � vis�vel, se n�o � vis�vel, ent�o est� fora da tela, logo destrua-se
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
            collision.gameObject.GetComponent<EnemyEntity>().LoseHealth(shotDamage);
        }else
        {
            collision.gameObject.GetComponent<PlayerController>().LoseHealth(shotDamage);
        }
    }
}
