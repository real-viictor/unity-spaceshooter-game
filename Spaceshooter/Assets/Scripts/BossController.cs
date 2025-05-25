using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : Entity
{
    [SerializeField] private Transform[] shotPositions;
    [SerializeField] private GameObject[] shotObjects;

    [SerializeField] private float shotSpeed;

    private bool isFightOcurring, isAttackPatternActive, isChangedDirection;

    private Rigidbody2D bossRB;

    private int attackPatternState;
    private float stateMachineTimer;
    private float shotIntervalTimer = 0.5f;
    private int shootingFromLeft = 0;

    private float bossSpeed = 3;

    private float xLimit = 5;
    private int directionChangeCounter = 0;

    private Vector2 bossRoamDirection;

    // Start is called before the first frame update
    void Start()
    {
       canBeHit = false;
       isAttackPatternActive = false;
       stateMachineTimer = Random.Range(1f, 4f);
       bossRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (isFightOcurring)
        {
            if(stateMachineTimer > 0f && !isAttackPatternActive)
            {
                stateMachineTimer -= Time.deltaTime;
            } else 
            { 
                stateMachineTimer = Random.Range(1f, 3f);
                attackPatternState = Random.Range(1, 1); //TODO: Alterar Range para total de ataques do Switch
                isAttackPatternActive = true;
            }

            if(isAttackPatternActive)
            {
                switch(attackPatternState)
                {
                    case 1:
                        RoamAttack();
                        break;
                }

            }
        }
    }

    private void RoamAttack()
    {
        if (shotIntervalTimer > 0f)
        {
            shotIntervalTimer -= Time.deltaTime;
        } else
        {
            shootingFromLeft ^= 1;
            CreateShot(shotObjects[0], shotPositions[0+shootingFromLeft], Vector2.down, shotSpeed);
            shotIntervalTimer = 0.3f;
        }

        if (bossRoamDirection == Vector2.zero)
        {
            float direction = Random.value < 0.5f ? -1 : 1;
            bossRoamDirection = new Vector2(direction, 0);
        }

        bossRB.velocity = bossRoamDirection * bossSpeed;

        if (bossRB.position.x > xLimit)
        {
            if (!isChangedDirection)
            {
                directionChangeCounter++;
                bossRoamDirection = Vector2.left;
                isChangedDirection = true;
            }
            
        } else if(bossRB.position.x < -xLimit)
        {
            if (!isChangedDirection)
            {
                directionChangeCounter++;
                bossRoamDirection = Vector2.right;
                isChangedDirection = true;
            }
        } else
        {
            isChangedDirection = false;
        }

        if (directionChangeCounter > 4)
        {
            directionChangeCounter = 0;
            bossRoamDirection = Vector2.zero;
            bossRB.velocity = bossRoamDirection;
            isAttackPatternActive = false;
        }
        
    }

    //Utilizado como evento da animação
    private void StartFight()
    {
        canBeHit = true;
        isFightOcurring = true;
        GetComponent<Animator>().enabled = false;
    }
}
