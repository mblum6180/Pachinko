using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameManager gameManager;  // Reference to the GameManager
    public float spawnYPosition = -1.9f;
    public float spawnXPosition = -0.9f;
    public float spawnZPosition = 10f;

    public float minImpulseForce = 0.3f; // Minimum impulse force
    public float maxImpulseForce = 0.7f; // Maximum impulse force

    public float minImpulseAngleDegrees = 90f; // Minimum impulse angle
    public float maxImpulseAngleDegrees = 130f; // Maximum impulse angle

    public float minSpawnInterval = 0.45f;
    public float maxSpawnInterval = 0.55f;

    public float minPitch = 0.8f;  // Minimum pitch value
    public float maxPitch = 1.2f;  // Maximum pitch value

    public bool canSpawn = true;
    // Time after which canSpawn will be forced back to true if it is still false
    public float watchdogTimer = 10f;

    public float powerCurve = 2f; // Exponential power curve factor. Adjust this in the Inspector.
    
    public bool isSpawning = false;

    public void ToggleSpawning(bool isEnabled)
    {
        isSpawning = isEnabled;
        if (isEnabled)
        {
            // Start the repeated spawning of balls
            InvokeRepeating("SpawnRandomBall", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
        }
        else
        {
            // Cancel the repeated spawning of balls
            CancelInvoke("SpawnRandomBall");
        }
    }




    private AudioSource audioSource;  // Reference to the AudioSource

    void Start()
    {

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Start the repeated spawning of balls
        //InvokeRepeating("SpawnRandomBall", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));

        // Start the watchdog timer
        InvokeRepeating("WatchdogResetCanSpawn", watchdogTimer, watchdogTimer);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     SpawnBall(1);
        // }
    }

    void WatchdogResetCanSpawn()
    {
        if (!canSpawn)
        {
            Debug.Log("Watchdog timer reset canSpawn to true");
            canSpawn = true;
        }
    }

    void SpawnRandomBall()
    {
    SpawnBall(1);

        // Reschedule the next call to get a random interval
        CancelInvoke("SpawnRandomBall");
        InvokeRepeating("SpawnRandomBall", Random.Range(minSpawnInterval, maxSpawnInterval), 
                    Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    public void SpawnBall(float power)
    {
        // Check if there are balls remaining
        if (gameManager.ballCount > 0 && canSpawn)
        {
            Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, spawnZPosition);
        
            // Instantiate the ball and get its Rigidbody2D component
            GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        
            // If the ball has a Rigidbody2D, apply an impulse force
            if (ballRigidbody != null)
            {
                // Generate a random impulse force and angle
                // Apply the power curve to the power value
                float adjustedPower = maxImpulseForce * Mathf.Pow(power, powerCurve);

                // Calculate the impulse force within the min and max range
                float impulseForce = Mathf.Lerp(minImpulseForce, adjustedPower, power);
                
                //float impulseForce = (Random.Range(minImpulseForce, maxImpulseForce) * power);
                float impulseAngleDegrees = Random.Range(minImpulseAngleDegrees, maxImpulseAngleDegrees);
            
                // Convert the impulse angle to radians and create a direction vector
                float impulseAngleRadians = impulseAngleDegrees * Mathf.Deg2Rad;
                Vector2 impulseDirection = new Vector2(Mathf.Cos(impulseAngleRadians), Mathf.Sin(impulseAngleRadians));

                ballRigidbody.AddForce(impulseDirection * impulseForce, ForceMode2D.Impulse);
            }

            // Decrement the ball count
            gameManager.DecreaseBallCount();

            canSpawn = false;

            CancelInvoke("WatchdogResetCanSpawn");
            InvokeRepeating("WatchdogResetCanSpawn", watchdogTimer, watchdogTimer);


            // Randomly adjust the pitch
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            // Play the spawn sound
            audioSource.Play();
        }
        else
        {
            // Display game over message and stop spawning balls
            //Debug.Log("Game Over!");
            // TODO: Display "Game Over" in the game scene
        }
    }
}
