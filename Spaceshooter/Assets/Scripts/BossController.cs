using UnityEngine;

public class BossController : Entity
{
    private bool isFightOcurring, isAttackPatternActive;

    private int attackPatternState;
    private float stateMachineTimer;

    // Start is called before the first frame update
    void Start()
    {
       canBeHit = false;
       stateMachineTimer = Random.Range(1f, 4f);
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
                attackPatternState = Random.Range(1, 5);
                isAttackPatternActive = true;
            }

            if(isAttackPatternActive)
            {
                switch(attackPatternState)
                {
                    case 1:
                        Debug.Log("Caso 1");
                        break;
                    case 2:
                        Debug.Log("Caso 2");
                        break; 
                    case 3:
                        Debug.Log("Caso 3");
                        break;
                    case 4:
                        Debug.Log("Caso 4");
                        break;
                }

                isAttackPatternActive = false;
            }
        }
    }

    //Utilizado como evento da animação
    private void StartFight()
    {
        canBeHit = true;
        isFightOcurring = true;
    }
}
