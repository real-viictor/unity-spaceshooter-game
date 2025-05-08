using UnityEngine;

public class BossController : Entity
{
    private bool isFightStarted;

    // Start is called before the first frame update
    void Start()
    {
       canBeHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (isFightStarted)
        {
            //TODO: State Machine
            Debug.Log("Attack");
        }
    }

    //Utilizado como evento da animação
    private void StartFight()
    {
        canBeHit = true;
        isFightStarted = true;
    }
}
