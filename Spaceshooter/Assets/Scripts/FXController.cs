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
        //Destruindo o objeto de efeito após o fim da animação, chamando ele como um Animation Event da animação
        Destroy(gameObject);
    }
}
