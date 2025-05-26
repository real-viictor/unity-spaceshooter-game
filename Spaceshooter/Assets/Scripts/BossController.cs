using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : Entity
{
    [SerializeField] private Transform[] shotPositions;
    [SerializeField] private GameObject[] shotObjects;

    [SerializeField] private float shotSpeed;

    private bool isFightOcurring, isAttackPatternActive, isChangedDirection, isBossInPosition;

    private Rigidbody2D bossRB;

    private int attackPatternState;
    private float stateMachineTimer;
    private float shotIntervalTimer = 0.5f;
    private int shootingFromLeft = 0;

    private float bossSpeed = 3;

    private float xLimit = 6.5f;
    private float yMinLimit = 1f;
    private float yMaxLimit = 3f;
    private int directionChangeCounter = 0;
    private int cannonShotsCounter, cannonShotsAttacksCounter;

    private Vector2 bossRoamDirection, bossTargetPosition;
    private Vector2 bossCenterPosition = new Vector2(0, 2);

    private bool hasBossRoamDirection, hasBossTargetPosition, hasToRecenterBoss;

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
                //Debug.Log("O timer está ativo");
                stateMachineTimer -= Time.deltaTime;
            } else 
            {
                if(stateMachineTimer < 0f)
                {
                    int newPattern;
                    do
                    {
                        newPattern = Random.Range(1, 3); //TODO: Alterar Range para total de ataques do Switch
                    } while (newPattern == attackPatternState);
                    attackPatternState = newPattern;
                }
                //Debug.Log("O timer está parado");
                stateMachineTimer = Random.Range(1f, 3f);
                isAttackPatternActive = true;
            }

            if(isAttackPatternActive)
            {
                switch(attackPatternState)
                {
                    case 1:
                        RoamAttack();
                        break;
                    case 2:
                        CannonShotAttack();
                        break;
                    
                }

            }
        }
    }

    private void RoamAttack()
    {
        if (!hasToRecenterBoss)
        {
            ShootAtIntervals();
            MoveBoss();
        } else
        {
            RecenterBoss();
        }
        

        void ShootAtIntervals()
        {
            if (shotIntervalTimer > 0f)
            {
                shotIntervalTimer -= Time.deltaTime;
            }
            else
            {
                shootingFromLeft ^= 1;
                CreateShot(shotObjects[0], shotPositions[0 + shootingFromLeft], Vector2.down, shotSpeed);
                shotIntervalTimer = 0.3f;
            }
        }
        
        void MoveBoss()
        {
            if (!hasBossRoamDirection)
            {
                float direction = Random.value < 0.5f ? -1 : 1;
                bossRoamDirection = new Vector2(direction, 0);
                hasBossRoamDirection = true;
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

            }
            else if (bossRB.position.x < -xLimit)
            {
                if (!isChangedDirection)
                {
                    directionChangeCounter++;
                    bossRoamDirection = Vector2.right;
                    isChangedDirection = true;
                }
            }
            else
            {
                isChangedDirection = false;
            }

            if (directionChangeCounter > 4)
            {
                directionChangeCounter = 0;
                bossRB.velocity = Vector2.zero;
                hasBossRoamDirection = false;
                hasToRecenterBoss = true;
            }
        }   
        
    }

    private void CannonShotAttack()
    {
        if (cannonShotsAttacksCounter < 3 && !hasToRecenterBoss)
        {
            PositionBoss();
            CannonShoot();
        } else
        {
            cannonShotsAttacksCounter = 0;
            hasToRecenterBoss = true;
        }

        RecenterBoss();

        void PositionBoss()
        {
            if (!hasBossTargetPosition)
            {
                bossTargetPosition = new Vector2(Random.Range(-xLimit, xLimit), Random.Range(yMinLimit, yMaxLimit));
                hasBossTargetPosition = true;
            }

            if (Vector2.Distance(transform.position, bossTargetPosition) < 0.01f)
            {
                isBossInPosition = true;
            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, bossTargetPosition, bossSpeed * Time.deltaTime);
            }
        }
        
        void CannonShoot()
        {
            if (isBossInPosition)
            {
                if (shotIntervalTimer > 0f)
                {
                    shotIntervalTimer -= Time.deltaTime;
                }
                else
                {
                    Vector2 shotDirection = new Vector2(Random.Range(-0.2f, 0.2f), -1);
                    float shotRotation = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg + 90;
                    CreateShot(shotObjects[1], shotPositions[2], shotDirection, shotSpeed * 1.5f, shotRotation);

                    cannonShotsCounter++;
                    shotIntervalTimer = 0.3f;
                }

                if (cannonShotsCounter == 3)
                {
                    cannonShotsCounter = 0;
                    isBossInPosition = false;
                    hasBossTargetPosition = false;
                    cannonShotsAttacksCounter++;
                }
            }
        }
    }

    private void RecenterBoss()
    {
        if (hasToRecenterBoss)
        {
            if (Vector2.Distance(transform.position, bossCenterPosition) < 0.01f)
            {
                hasToRecenterBoss = false;
                isAttackPatternActive = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, bossCenterPosition, bossSpeed * Time.deltaTime);
            }
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
