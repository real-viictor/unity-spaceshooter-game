using UnityEngine;

public class BossDefeatController : Entity
{
    [SerializeField] private GameObject smallExplosion;
    [SerializeField] private Transform[] explosionSpots;

    private GameManager gameManager;

    private float explosionCreationTimer;
    private int lastExplosionIndex, currentExplosionIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateExplosion();
    }

    private void OnDestroy()
    {
        gameManager.ReturnToMenu();
    }

    private void CreateExplosion()
    {
        if (explosionCreationTimer > 0f)
        {
            explosionCreationTimer -= Time.deltaTime;
        } else
        {
            do
            {
                currentExplosionIndex = Random.Range(0, explosionSpots.Length);
            } while (currentExplosionIndex == lastExplosionIndex);

            Instantiate(smallExplosion, explosionSpots[currentExplosionIndex]);
            lastExplosionIndex = currentExplosionIndex;
            explosionCreationTimer = 0.3f;
        }
        
    }
}
