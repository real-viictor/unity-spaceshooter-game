using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 10f;

    private Rigidbody2D shootRB;

    // Start is called before the first frame update
    void Start()
    {
        shootRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        shootRB.velocity = Vector2.up * shootSpeed;
    }
}
