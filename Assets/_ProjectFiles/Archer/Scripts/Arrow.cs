using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D arrowRB;

    void Start()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        arrowRB.simulated = false;
    }

    void Update()
    {
        Vector2 movementVector = arrowRB.velocity;

       
        if (arrowRB.simulated)
        {
            transform.right = movementVector;   
        }   
    }
}
