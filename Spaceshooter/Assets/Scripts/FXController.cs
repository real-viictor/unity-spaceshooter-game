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
        //Destruindo o objeto de efeito após o fim da animação, chamando ele como um Animation Event da animação
        Destroy(gameObject);
    }
}
