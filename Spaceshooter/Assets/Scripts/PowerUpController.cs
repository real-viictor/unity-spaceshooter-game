using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private float powerUpDuration = 10f;
    private float timer = 0f;

    private float blinkTimer = 0f;
    private float blinkInterval = 0.2f;

    private Renderer rend;

    private Rigidbody2D rb;

    [SerializeField] private string powerUpEffect;

    // Start is called before the first frame update
    void Start()
    {
        SetUpPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        
        DestroyPowerUp();
    }

    private void SetUpPowerUp()
    {
        rend = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            rb.velocity = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.5f, -0.7f));
        }
    }

    private void DestroyPowerUp()
    {
        timer += Time.deltaTime;

        if(timer >= powerUpDuration * 0.66f) {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                rend.enabled = !rend.enabled;
                blinkTimer = 0f;
            }
        }

        if (timer >= powerUpDuration)
        {
            Destroy(gameObject);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MainPlayer"))
        {
            switch (powerUpEffect)
            {
                case "health":
                    collision.gameObject.GetComponent<PlayerController>().AddHealth(1);
                    break;
                case "shot":
                    collision.gameObject.GetComponent<PlayerController>().UpgradeShotLevel();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
