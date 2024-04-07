using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Instanciando vari�vel que cont�m o Rigidbody do player
    private Rigidbody2D rigidbody;
    //Determinando velocidade do player
    [SerializeField] private float playerSpeed = 5f;

    //Vari�veis que guardar�o a velocidade vertical e horizontal do player
    private float horizontalSpeed;
    private float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Coletando o Rigidbody do player
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Movendo o Player
    private void Move()
    {
        //Detectando se o jogador est� pressionando os bot�es de movimento e multiplicando pela velocidade padr�o do player
        //NOTE: Os bot�es de movimento s�o os definidos no Input Manager, em "Edit > Project Settings > InputManager"
        horizontalSpeed = Input.GetAxis("Horizontal") * playerSpeed;
        verticalSpeed = Input.GetAxis("Vertical") * playerSpeed;

        //adicionando velocidade pela vari�vel velocity do Rigidbody, passando as velocidades calculadas anteriormente como vetor
        rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}
