using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private float positionX, positionY;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float spawnMoveDuration = 4f; 
    private float spawnMoveElapsedTime = 0f;

    private float powerUpDuration = 6f;
    private float timer = 0f;

    private float blinkTimer = 0f;
    private float blinkInterval = 0.2f;

    private Renderer rend;

    [SerializeField] private string powerUpEffect;

    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        startPosition = transform.position;
        positionX = Random.Range(-8f,8f);
        positionY = Random.Range(-0.5f, -4.5f);
        targetPosition = new Vector3(positionX, positionY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PositionPowerUp();
        DestroyPowerUp();
    }

    private void PositionPowerUp()
    {
        if (isMoving)
        {
            spawnMoveElapsedTime += Time.deltaTime;
            float t = spawnMoveElapsedTime / spawnMoveDuration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void DestroyPowerUp()
    {
        if (!isMoving)
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
