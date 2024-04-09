using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

    //Vari�vel que guarda o Rigidbody do Player
    private Rigidbody2D shotRB;

    //Vari�vel de controle que guarda a informa��o se o tiro est� vis�vel ou n�o
    private bool isVisible;

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
        //Verificando se o componente � vis�vel utilizando o isVisible no SpriteRenderer do filho do gameObject
        isVisible = GetComponentInChildren<SpriteRenderer>().isVisible;

        //Se n�o � vis�vel, ent�o est� fora da tela, ent�o destruir-se
        if (!isVisible) {
            Destroy(gameObject);
        }
    }
}
