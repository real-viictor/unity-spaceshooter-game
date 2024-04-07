using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{   
    //Velocidade default do tiro
    [SerializeField] private float shootSpeed = 10f;

    //Vari�vel que guarda o Rigidbody do Player
    private Rigidbody2D shootRB;

    // Start is called before the first frame update
    void Start()
    {
        //Acessando Rigidbody
        shootRB = GetComponent<Rigidbody2D>();

        //Fazendo o tiro se mover pra cima
        //NOTE: Como a velocidade do tiro � constante e ele est� sendo movido pelo Rigidbody, ele n�o precisa ficar no Update.
        shootRB.velocity = Vector2.up * shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenOutside();
    }

    private void DestroyWhenOutside()
    {
        if(transform.position.y > 5.2f) {
            Destroy(gameObject);
        }
    }
}
