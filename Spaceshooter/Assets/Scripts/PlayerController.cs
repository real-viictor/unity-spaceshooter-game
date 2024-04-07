using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Instanciando variável que contém o Rigidbody do player
    private Rigidbody2D rigidbody;
    //Determinando velocidade do player
    [SerializeField] private float playerSpeed = 5f;

    //Variáveis que guardarão a velocidade vertical e horizontal do player
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
        //Detectando se o jogador está pressionando os botões de movimento
        //NOTE: Os botões de movimento são os definidos no Input Manager, em "Edit > Project Settings > InputManager"
        horizontalSpeed = Input.GetAxis("Horizontal");
        verticalSpeed = Input.GetAxis("Vertical");

        //adicionando velocidade pela variável velocity do Rigidbody e multiplicando pela velocidade padrão do player
        //NOTE: O rigidbody já ajusta pelo Time.DeltaTime
        rigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed) * playerSpeed;
    }
}
