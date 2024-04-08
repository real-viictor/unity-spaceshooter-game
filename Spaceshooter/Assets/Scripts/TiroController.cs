using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

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
        //Checando se está fora da tela
        if(transform.position.y > 5.2f || transform.position.y < -5.2f) {
            Destroy(gameObject);
        }
    }
}
