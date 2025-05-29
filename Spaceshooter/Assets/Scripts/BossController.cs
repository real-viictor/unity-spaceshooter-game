using UnityEngine;

public class BossController : Entity
{
    [SerializeField] private Transform[] shotPositions;
    [SerializeField] private GameObject[] shotObjects;
    [SerializeField] private float shotSpeed;

    private PlayerController enemyShotTarget;

    private bool isFightOcurring, isAttackPatternActive, isChangedDirection, isBossInPosition;

    
    private Rigidbody2D bossRB;

    private int attackPatternState;
    private float stateMachineTimer;
    private int shootingFromLeft = 0;

    private float roamShotTimer;
    private float cannonShotTimer;
    private float missileShotTimer;

    //Variáveis que salvam o tempo padrão de delay entre os tiros dos ataques do boss
    [SerializeField] private float standardRoamShotTimer = 0.3f;
    [SerializeField] private float standardCannonShotTimer = 0.2f;
    [SerializeField] private float standardMissileShotTimer = 0.3f;

    private float bossSpeed = 3;

    private float xLimit = 6.5f;
    private float yMinLimit = 1f;
    private float yMaxLimit = 3f;
    private int directionChangeCounter = 0;
    private int cannonShotsCounter, cannonShotsAttacksCounter, missileShotsCounter;

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
       enemyShotTarget = FindObjectOfType<PlayerController>();
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
                if(stateMachineTimer < 0f)
                {
                    int newPattern;
                    do
                    {
                        newPattern = Random.Range(1, 4); //TODO: Alterar Range para total de ataques do Switch
                    } while (newPattern == attackPatternState);
                    attackPatternState = newPattern;
                }

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
                    case 3:
                        MissilesAttack();
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
            if (roamShotTimer > 0f)
            {
                roamShotTimer -= Time.deltaTime;
            }
            else
            {
                shootingFromLeft ^= 1;
                CreateShot(shotObjects[0], shotPositions[0 + shootingFromLeft], Vector2.down, shotSpeed);
                roamShotTimer = standardRoamShotTimer;
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

            if (isAtPosition(transform.position, bossTargetPosition))
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
                if (cannonShotTimer > 0f)
                {
                    cannonShotTimer -= Time.deltaTime;
                }
                else
                {
                    Vector2 shotDirection = new Vector2(Random.Range(-0.2f, 0.2f), -1);
                    float shotRotation = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg + 90;
                    CreateShot(shotObjects[1], shotPositions[2], shotDirection, shotSpeed * 1.5f, shotRotation);

                    cannonShotsCounter++;
                    cannonShotTimer = standardCannonShotTimer;
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

    private void MissilesAttack()
    {
        if(missileShotsCounter <= 10 && !hasToRecenterBoss)
        {
            PositionBoss();
            ShotMissiles();
        } else
        {
            missileShotsCounter = 0;
            hasToRecenterBoss = true;
        }

        RecenterBoss();

        void PositionBoss()
        {
            if(!hasBossTargetPosition)
            {
                bossTargetPosition = new Vector2(0, 3);
                hasBossTargetPosition = true;
            }
            

            if (isAtPosition(transform.position, bossTargetPosition))
            {
                isBossInPosition = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, bossTargetPosition, bossSpeed * Time.deltaTime);
            }
        }

        void ShotMissiles()
        {
            if(isBossInPosition)
            {
                if(missileShotTimer > 0) 
                {
                    missileShotTimer -= Time.deltaTime;
                } else
                {
                    if (enemyShotTarget != null)
                    {
                        Vector2 shotDirection = enemyShotTarget.transform.position - shotPositions[2].transform.position;
                        float shotRotation = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg + 90;
                        CreateShot(shotObjects[1], shotPositions[2], shotDirection.normalized, shotSpeed, shotRotation);
                    }

                    missileShotsCounter++;
                    missileShotTimer = standardMissileShotTimer;
                }
            }
        }
    }

    private void RecenterBoss()
    {
        if (hasToRecenterBoss)
        {
            if (isAtPosition(transform.position, bossCenterPosition))
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

    private bool isAtPosition(Vector2 currentPosition, Vector2 targetPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) < 0.01f;
    }

    //Utilizado como evento da animação
    private void StartFight()
    {
        canBeHit = true;
        isFightOcurring = true;
        GetComponent<Animator>().enabled = false;
    }
}
