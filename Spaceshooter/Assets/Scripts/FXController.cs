using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destruindo o objeto de efeito ap�s o fim da anima��o, passando o tempo da anima��o como timer
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
