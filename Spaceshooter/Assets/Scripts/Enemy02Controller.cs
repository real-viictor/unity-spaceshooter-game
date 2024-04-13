using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : EnemyEntity
{
    private Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando o RB do inimigo
        enemyRB = GetComponent<Rigidbody2D>();

        //Definindo movimentação do inimigo para baixo
        enemyRB.velocity = Vector2.down * enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
