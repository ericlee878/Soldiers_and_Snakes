using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // rigid body of bullet
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Disable gravity for the bullet (so the bullet is not pulled to the ground)
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnCollisionEnter2D()
    {
        //// Disappear once it hits an object
        //OnBecameInvisible();
        Destroy(gameObject);
    }


}
