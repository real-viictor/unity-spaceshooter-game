using UnityEngine;

public class ShieldController : Entity
{
    private PlayerController playerObject;
    private Transform playerPosition;
    private int shieldProtection;
    private SpriteRenderer spriteRend;

    private float shieldDuration = 4f;
    private float shieldTimer;

    [SerializeField] private Sprite[] shieldSprites;

    [SerializeField] private AudioClip shieldUpSound;
    [SerializeField] private AudioClip shieldDownSound;

    // Start is called before the first frame update
    void Start()
    {
        SetUpShield();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        ReduceShieldLifetime();
    }

    private void SetUpShield()
    {
        AudioSource.PlayClipAtPoint(shieldUpSound, Vector3.zero);
        shieldTimer = shieldDuration;
        playerObject = FindObjectOfType<PlayerController>();
        playerPosition = playerObject.GetComponent<Transform>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        shieldProtection = entityHealth;
    }

    private void FollowPlayer()
    {
        transform.position = playerPosition.position;
    }

    public void UpdateSprite()
    {
        if (entityHealth == 1)
        {
            spriteRend.sprite = shieldSprites[2];
        } else if(entityHealth <= shieldProtection/2)
        {
            spriteRend.sprite = shieldSprites[1];
        } else
        {
            spriteRend.sprite = shieldSprites[0];
        }
    }

    private void ReduceShieldLifetime()
    {
        if(shieldTimer >= 0)
        {
            shieldTimer -= Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }

        Color color = spriteRend.color;

        color.a = Mathf.Clamp(shieldTimer / 1f, 0f, 1f);
        spriteRend.color = color;
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    private void OnDestroy()
    {
        playerObject.setShieldStatus(false);
        AudioSource.PlayClipAtPoint(shieldDownSound, Vector3.zero);
    }
}
