using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // rigid body of snake
    public Rigidbody2D rb;

    // sprite renderer of snake
    public SpriteRenderer spriteRender;

    // current health of snake
    public float currentHealth = 1.0f;

    // Accesses Soldier 
    private Soldier soldier;

    /// Transform from the player object
    /// Used to find the player's position
    private Transform soldierTransform;



    /// Vector from us to the player
    private UnityEngine.Vector2 OffsetToPlayer => soldierTransform.position - transform.position;

    /// Unit vector in the direction of the player, relative to us
    private UnityEngine.Vector2 HeadingToPlayer => OffsetToPlayer.normalized;

    // Shooting Range for Snake
    private float shootingRange = 2.0f;

    // Single ScoreManager GameObject
    public static ScoreManager scoreManager;

    // checks if player is alive of dead
    public bool isDead;



    // For shooting ever () second

    // Initializes time last shot
    private float timeSinceLastShot = 0.0f;
    // Initializes shoot interval for snake
    private float shootInterval = 5.0f;





    // Bullet 
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float bulletSpeed = 10.0f;  // Adjust the bullet speed as needed
    public float damage = 1.0f; // Adjust the damage as needed



    // Start is called before the first frame update
    void Start()
    {
        soldier = FindObjectOfType<Soldier>();

        soldierTransform = soldier.transform;

        rb = GetComponent<Rigidbody2D>();

        // Disable gravity for the snake (so the snake is not pulled to the ground)
        rb.gravityScale = 0f;

        // Lock rotation in the Z-axis to prevent flipping
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Gets the sprite renderer from Unity
        spriteRender = GetComponent<SpriteRenderer>();

        // Gets the ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();

        // Sets the isDead variable
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        flip();

        // Freezes rotation
        rb.freezeRotation = true;

        // Accesses the snake's position
        float snakeXCoordinate = this.transform.position.x;
        float snakeYCoordinate = this.transform.position.y;
        UnityEngine.Vector2 snakePosition = new UnityEngine.Vector2(snakeXCoordinate, snakeYCoordinate);

        // Accesses the player's position
        UnityEngine.Vector2 soldierPosition = new Vector2(soldierTransform.position.x, soldierTransform.position.y);
        // Calculate the distance between the snake and the player
        float distanceToPlayer = Vector2.Distance(snakePosition, soldierPosition);

        //if (distanceToPlayer <= shootingRange) // If close enough to shoot
        //{
        //    // Stops snake
        //    rb.velocity = new UnityEngine.Vector2(0, 0);


        //    // Check if enough time has passed since the last shot
        //    if (Time.time - timeSinceLastShot >= shootInterval)
        //    {
        //        // Attack the player
        //        Shoot();
        //        // Update the time of the last shot
        //        timeSinceLastShot = Time.time;
        //    }
        //}
        //else // If not in shooting range
        //{ 
        // Makes the snake head towards the player
        UnityEngine.Vector3 directionToPlayer = new UnityEngine.Vector3(HeadingToPlayer.x, HeadingToPlayer.y, 0.0f);

        // Clamp the snake's position within the specified range
        snakePosition.x = Mathf.Clamp(snakePosition.x, -9.0f, 9.0f);
        snakePosition.y = Mathf.Clamp(snakePosition.y, -4.0f, 4.0f);

        // Apply the clamped position back to the GameObject's position
        rb.position = snakePosition;

        // Sets new Enemy's velocity
        rb.velocity = directionToPlayer;
        //}

        if (soldier.isDead)
        {
            rb.velocity = new UnityEngine.Vector2(0f, 0f);
        }
    }

    // Sets orientation of sprite
    void flip()
    {
        if (OffsetToPlayer.x < 0f)
        {
            spriteRender.flipX = false;
        }
        else if (OffsetToPlayer.x > 0f)
        {
            spriteRender.flipX = true;
        }
    }


    // Shoots bullets at player
    void Shoot()
    {
        // Offset to the right
        Vector3 spawnOffset = new Vector3(HeadingToPlayer.x / 10.0f, HeadingToPlayer.y / 10.0f, 0f);

        // Changes spawn position of bullet
        Vector3 spawnPosition = transform.position + spawnOffset;

        // Adjust the bullet's spawn position so that it wouldn't hit itself
        spawnPosition += spawnOffset;

        // Create a new bullet instance from the prefab
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Get the Rigidbody2D component of the bullet
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity to move it in the desired direction
        bullet_rb.velocity = HeadingToPlayer * bulletSpeed;

        // Disable gravity for the bullet (so the bullet is not pulled to the ground)
        bullet_rb.gravityScale = 0f;
    }

    // Check collisions to see if hit by bullet
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Disappear once it hits a bullet
        if (collision.collider.gameObject.GetComponent<Bullet>())
        {
            OnBecameInvisible();
        }
    }

    // When hit with bullet
    void OnBecameInvisible()
    {
        // Increases score
        scoreManager.IncrementScore(1);

        // Destroys this object
        Destroy(gameObject);
    }
}
