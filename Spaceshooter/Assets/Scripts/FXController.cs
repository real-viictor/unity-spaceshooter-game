using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyFX()
    {
        //Destruindo o objeto de efeito ap�s o fim da anima��o, chamando ele como um Animation Event da anima��o
        Destroy(gameObject);
    }
}
