using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shootSpeed = 10f;

    //Variável que guarda o Rigidbody do Player
    private Rigidbody2D shootRB;

    [SerializeField] private string shootOrigin;

    // Start is called before the first frame update
    void Start()
    {
        //Acessando Rigidbody
        shootRB = GetComponent<Rigidbody2D>();

        MoveShoot();    
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

    //Atirando para o lado dependendo da entidade que gerou o tiro
    private void MoveShoot()
    {
        //Fazendo o tiro se mover pra cima
        //NOTE: Como a velocidade do tiro é constante e ele está sendo movido pelo Rigidbody, ele não precisa ficar no Update.
        if (shootOrigin == "Player")
        {
            shootRB.velocity = Vector2.up * shootSpeed;
        }
        else
        {
            shootRB.velocity = Vector2.down * shootSpeed;
        }
    }
}
