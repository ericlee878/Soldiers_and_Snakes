using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    // checks if player is alive of dead
    public bool isDead;

    // rigid body of soldier
    public Rigidbody2D rb;

    // sprite renderer of soldier
    public SpriteRenderer spriteRender;

    // Animator reference
    public Animator animator;

    // Adjust the speed as needed
    public float moveSpeed = 5.0f;

    // current health of soldier
    public float currentHealth = 15.0f;

    // jump force of soldier
    public float jumpForce = 5.0f;


    // Bullet 
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float bulletSpeed = 10.0f;  // Adjust the bullet speed as needed
    public float damage = 1.0f; // Adjust the damage as needed



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent <Animator>();
        isDead = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            isDead = false;
        }

        if (!isDead) // player does not move if is dead
        {
            move();  // Moves the soldier
            checkIfShoot(); // Checks if soldier is shooting

            // Control animations based on your game object's behavior
            animator.SetBool("run", rb.velocity.magnitude > 0);  // Set to true when moving.
            animator.SetBool("shoot", Input.GetKeyDown(KeyCode.Space));  // Define the condition for shooting.
            animator.SetBool("dead", isDead);  // Character dies.
        }
    }

    // Flips the sprite if it is facing the other direction
    void flip()
    {
        // Gets horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0f)
        {
            spriteRender.flipX = true;
        }
        else if (horizontalInput > 0f)
        {
            spriteRender.flipX = false;
        }
    }

    void move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, verticalInput * moveSpeed, 0);

        // Update the soldier's velocity
        rb.velocity = movement;

        // Clamp the soldier's position within certain boundaries
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -9f, 9f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f);

        transform.position = newPosition;


        // Freezes rotation
        rb.freezeRotation = true;

        flip();
    }



    // Checks if soldier is shooting
    void checkIfShoot()
    {
        // Check for spacebar key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();  // Call the shoot function when space is pressed
        }
    }



    // Shooting bullets
    void Shoot()
    {
        // Gets horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a new bullet instance from the prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody2D component of the bullet
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        // Reset the velocity to zero
        bullet_rb.velocity = Vector2.zero;

        Vector3 spawnOffset = Vector3.zero;

        if (spriteRender.flipX == true)
        {
            // Set the bullet's velocity to move it in the desired direction
            bullet_rb.velocity = -transform.right * bulletSpeed;
            spawnOffset = new Vector3(-0.7f, 0f, 0f); // Offset to the left
        }
        else if (spriteRender.flipX == false)
        {
            // Set the bullet's velocity to move it in the desired direction
            bullet_rb.velocity = transform.right * bulletSpeed;
            spawnOffset = new Vector3(0.7f, 0f, 0f); // Offset to the right
        }


        // Adjust the bullet's spawn position
        bullet.transform.position += spawnOffset;

        // Destroy the bullet after a certain amount of time (e.g., 2 seconds)
        Destroy(bullet, 2f);
    }



    // Check collisions to see if hit by bullet
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Disappear once it hits an object
        if (collision.collider.gameObject.GetComponent<Snake>())
        {
            Die();
            //HitByBullet();
        }
    }



    //// When hit by bullet
    //void HitByBullet()
    //{
    //    // Decreases health
    //    currentHealth -= damage;

    //    // If current health reaches 0
    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }

    //}



    // When health reaches zero
    void Die()
    {
        isDead = true;
        //animator.SetBool("dead", isDead);  // Character dies.
    }


}
