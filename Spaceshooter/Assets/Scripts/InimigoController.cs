using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    private Rigidbody2D enemyRB;

    private float enemySpeed = 1.5f;

    private float shotTimer = 3f;

    private bool isInPosition = false;

    [SerializeField] private GameObject enemyShotObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
        Shoot();
    }

    private void Stop()
    {
        //Parando o inimigo na posição desejada da tela
        if (transform.position.y <= 3.5f)
        {
            enemyRB.velocity = Vector2.down * 0;
            isInPosition = true;
        }
    }

    private void Shoot(float shotDelay = 3f)
    {
        if(isInPosition)
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                shotTimer = shotDelay;
                Instantiate(enemyShotObject, new Vector3(transform.position.x, transform.position.y - 0.3f, 0), Quaternion.identity);
            }
        }
    }
}
