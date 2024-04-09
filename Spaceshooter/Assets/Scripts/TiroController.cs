using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

    //Variável que guarda o Rigidbody do Player
    private Rigidbody2D shotRB;

    //Variável de controle que guarda a informação se o tiro está visível ou não
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
        //Verificando se o componente é visível utilizando o isVisible no SpriteRenderer do filho do gameObject
        isVisible = GetComponentInChildren<SpriteRenderer>().isVisible;

        //Se não é visível, então está fora da tela, então destruir-se
        if (!isVisible) {
            Destroy(gameObject);
        }
    }
}
