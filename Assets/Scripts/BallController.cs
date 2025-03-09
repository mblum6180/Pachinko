using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;  // Reference to the GameManager

    public float destroyYPosition = -30f; // The y position at which the ball should be destroyedf;
    public float destroyAfterSeconds = 30f; // The time after which the ball should be destroyed

    public float minSpeedForSound = 2f; // Set this to the minimum speed for the sound to play
    private AudioSource audioSource;
    public float maxSpeed = 10f; // The maximum expected speed
    public float maxVolume = 1f; // The maximum volume

    public BallSpawner ballSpawner;

    private Vector3 lastPosition;
    private float timeSinceLastMove;


    void Awake()
    {
        // Automatically find the BallSpawner in the scene
        ballSpawner = FindFirstObjectByType<BallSpawner>();
    }

    void Start()
    {
        // Destroy the game object after a certain time
        Destroy(gameObject, destroyAfterSeconds);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Call the ResetCanSpawn function after a 2 second delay
        Invoke("ResetCanSpawn", 3f);

        lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            // The ball has moved since the last frame
            lastPosition = transform.position;
            timeSinceLastMove = 0f;
        }
        else
        {
            // The ball has not moved since the last frame
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove > 3f)
            {
                // The ball hasn't moved for more than 5 seconds
                NudgeBall();

                // Reset the timer
                timeSinceLastMove = 0f;
            }
        }

        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
            ballSpawner.canSpawn = true;
        }
    }

    // This function is called when the ball collides with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball hit a pin and is over the minimum speed
        if (collision.gameObject.tag == "Pin")
        {
            var speed = GetComponent<Rigidbody2D>().linearVelocity.magnitude;

            if (speed > minSpeedForSound)
            {
                // Calculate the volume based on the current speed
                float volume = (speed / maxSpeed) * maxVolume;
                // Cap the volume at maxVolume
                volume = Mathf.Min(volume, maxVolume);

                // Set the volume
                audioSource.volume = volume;

                // Play the sound
                audioSource.Play();
            }
        }
    }

    // This function is called when the ball enters a trigger zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            ballSpawner.canSpawn = true;
            CancelInvoke("ResetCanSpawn"); // Cancel the ResetCanSpawn call
            //Debug.Log("Target Hit");
        }
    }

    void ResetCanSpawn()
    {
        ballSpawner.canSpawn = true;
    }

    void NudgeBall()
    {
        Rigidbody2D ballRigidbody = GetComponent<Rigidbody2D>();
        if (ballRigidbody != null)
        {
            // Apply a small force to the ball
            ballRigidbody.AddForce(new Vector2(0.02f, 0.02f), ForceMode2D.Impulse);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveBallInstance(this.gameObject);
        GameManager.instance.GameOver();
    }

}