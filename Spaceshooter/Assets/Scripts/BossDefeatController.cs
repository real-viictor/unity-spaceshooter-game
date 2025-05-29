using UnityEngine;

public class BossDefeatController : MonoBehaviour
{
    [SerializeField] private GameObject smallExplosion, bossExplosion;
    [SerializeField] private Transform[] explosionSpots;

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

    private void DestroyBoss()
    {
        Destroy(gameObject);
        Instantiate(bossExplosion, transform.position, Quaternion.identity);
    }
}
