using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Entity
{
    //Instanciando vari�vel que cont�m o Rigidbody do player
    private Rigidbody2D playerRB;

    //Determinando velocidade do player
    [SerializeField] private float playerSpeed = 5f;

    //Vari�vel em Array que cont�m os objetos dos tiros (Baseado em niveis)
    [SerializeField] private GameObject[] shotObjects;

    //Vari�vel em Array que guarda todas as posi��es de origem dos tiros
    [SerializeField] private Transform[] shotPositions;

    //Velocidade default do tiro
    [SerializeField] private float shotSpeed = 10f;

    //N�vel do tiro do Player
    private int shotLevel = 1;

    [SerializeField] private int shieldCharges = 3;
    [SerializeField] private int maxShieldCharges = 5;

    [SerializeField] private GameObject shieldObject;

    [SerializeField] private bool canMove = true;

    private bool isShieldActive = false;
    
    //Vari�veis que guardar�o a velocidade vertical e horizontal do player
    private float horizontalSpeed, verticalSpeed;

    //Vari�vel de controle que faz o player atirar pela esquerda no n�vel 2 de tiro
    private int shootingFromLeft = 0;

    //Vari�veis que guardam o limite que o jogador pode chegar na tela
    private float hLimitPosition = 8.7f;
    private float vLimitPosition = 4.5f;

    private float upgradedShotTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Coletando o Rigidbody do player
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Move();
            Shoot();
            ReducePowerUpDuration();
            UseShield();
        }
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

        //Limita a posi��o do jogador nos limites da tela
        float clampedX = Mathf.Clamp(playerRB.position.x, -hLimitPosition, hLimitPosition);
        float clampedY = Mathf.Clamp(playerRB.position.y, -vLimitPosition, vLimitPosition);

        //Atualiza a posi��o travada
        //NOTE: Utiliza-se a transform.position para evitar "flicks" causados pela f�sica aplicada em playerRB
        transform.position = new Vector2(clampedX, clampedY);
    }

    //Disparando com o Player
    private void Shoot()
    {
        if (Input.GetButtonDown("Shoot"))
        { 

            if (shotLevel == 2)
            {
                shootingFromLeft ^= 1;
            } else
            {
                shootingFromLeft = 0;
            }

            CreateShot(shotObjects[shotLevel - 1], shotPositions[shotLevel - 1 + shootingFromLeft], Vector2.up, shotSpeed);
        }
    }

    private void UseShield()
    {
        if (Input.GetButtonDown("Shield"))
        {
            if(shieldCharges > 0 && !isShieldActive)
            {
                isShieldActive = true;
                Instantiate(shieldObject, transform.position, Quaternion.identity);
                shieldCharges--;
            }
        }
    }

    private void ReducePowerUpDuration()
    {
        if(shotLevel == 2)
        {
            upgradedShotTimer -= Time.deltaTime;

            if (upgradedShotTimer <= 0)
            {
                shotLevel = 1;
            }
        }
    }

    public void AddHealth(int points)
    {
        if(entityHealth < 10)
        {
            entityHealth += points;
        }
    }

    public void UpgradeShotLevel() 
    {
        upgradedShotTimer = 10f;
        shotLevel = 2;
    }

    public void AddShieldCharge(int charges)
    {
        
        shieldCharges = Mathf.Clamp(shieldCharges + charges, 0, maxShieldCharges);
    }

    public void setShieldStatus(bool status)
    {
        isShieldActive = status;
    }

    public void setCanMoveStatus(bool status)
    {
        canMove = status;
    }
}
