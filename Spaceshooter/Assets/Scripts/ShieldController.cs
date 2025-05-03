using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class ShieldController : Entity
{
    private Transform playerPosition;
    private int shieldProtection;
    private SpriteRenderer spriteRend;

    [SerializeField] private Sprite[] shieldSprites;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        shieldProtection = entityHealth;

        Debug.Log(spriteRend.sprite);
    }

    // Update is called once per frame
    void Update()
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

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
