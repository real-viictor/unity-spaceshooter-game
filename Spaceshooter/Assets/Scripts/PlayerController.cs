using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    //Instanciando vari�vel que cont�m o Rigidbody do player
    private Rigidbody2D playerRB;
    //Determinando velocidade do player
    [SerializeField] private float playerSpeed = 5f;

    //Vari�vel que cont�m o objeto do tiro
    [SerializeField] private GameObject shotObject;

    //Vari�vel que determina onde o tiro deve sair
    [SerializeField] private Transform shotPosition;

    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

    //Vari�veis que guardar�o a velocidade vertical e horizontal do player
    private float horizontalSpeed;
    private float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Coletando o Rigidbody do player
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    //Movendo o Player
    private void Move()
    {
        //Detectando se o jogador est� pressionando os bot�es de movimento (Usando o Input Raw para evitar "Smoothing")
        //NOTE: Os bot�es de movimento s�o os definidos no Input Manager, em "Edit > Project Settings > InputManager"
        horizontalSpeed = Input.GetAxisRaw("Horizontal");
        verticalSpeed = Input.GetAxisRaw("Vertical");

        //adicionando velocidade pela vari�vel velocity do Rigidbody, normalizando movimento vertical e multiplicando pela velocidade padr�o do player
        //NOTE: O rigidbody j� ajusta pelo Time.DeltaTime
        playerRB.velocity = new Vector2(horizontalSpeed, verticalSpeed).normalized * playerSpeed;
    }

    //Disparando com o Player
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Criando a inst�ncia do tiro e salvando na vari�vel
            GameObject shotInstance = Instantiate(shotObject, shotPosition.position, Quaternion.identity);

            //Ajustando a velocidade e dire��o do tiro
            shotInstance.GetComponent<Rigidbody2D>().velocity = Vector2.up * shotSpeed;
        }
    }
}
