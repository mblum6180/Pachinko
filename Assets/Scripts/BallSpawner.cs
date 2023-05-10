using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnYPosition = -1.9f;
    public float spawnXPosition = -0.9f;
    public float minXPosition = -4f;
    public float maxXPosition = 4f;
    public float spawnZPosition = 10f;

    public float minImpulseForce = 0.3f; // Minimum impulse force
    public float maxImpulseForce = 0.7f; // Maximum impulse force

    public float minImpulseAngleDegrees = 90f; // Minimum impulse angle
    public float maxImpulseAngleDegrees = 130f; // Maximum impulse angle

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        float randomX = Random.Range(minXPosition, maxXPosition);
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, spawnZPosition);
        
        // Instantiate the ball and get its Rigidbody2D component
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        
        // If the ball has a Rigidbody2D, apply an impulse force
        if (ballRigidbody != null)
        {
            // Generate a random impulse force and angle
            float impulseForce = Random.Range(minImpulseForce, maxImpulseForce);
            float impulseAngleDegrees = Random.Range(minImpulseAngleDegrees, maxImpulseAngleDegrees);
            
            // Convert the impulse angle to radians and create a direction vector
            float impulseAngleRadians = impulseAngleDegrees * Mathf.Deg2Rad;
            Vector2 impulseDirection = new Vector2(Mathf.Cos(impulseAngleRadians), Mathf.Sin(impulseAngleRadians));

            ballRigidbody.AddForce(impulseDirection * impulseForce, ForceMode2D.Impulse);
        }
    }
}
