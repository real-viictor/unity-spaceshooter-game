using UnityEngine;

public class FXController : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        if(explosionSound)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Vector3.zero);
        }
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
