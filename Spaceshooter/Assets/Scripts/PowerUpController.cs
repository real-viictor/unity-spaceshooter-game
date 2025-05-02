using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private float positionX, positionY;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float spawnMoveDuration = 4f; 
    private float spawnMoveElapsedTime = 0f;

    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        positionX = Random.Range(-8f,8f);
        positionY = Random.Range(-0.5f, -4.5f);
        targetPosition = new Vector3(positionX, positionY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PositionPowerUp();
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
}
